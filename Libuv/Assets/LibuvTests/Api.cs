using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Libuv
{
    public class ApiTest
    {
        [TearDown]
        public void TearDown()
        {
            bool leak = false;
            Api.uv_walk(Api.uv_default_loop(), (handle, arg) =>
            {
                leak = true;
                Api.uv_close(handle, null);
            }, IntPtr.Zero);
            Assert.AreEqual(0, Api.uv_run(Api.uv_default_loop(), RunMode.Default));
            Assert.AreEqual(0, Api.uv_loop_close(Api.uv_default_loop()));

            if (leak)
            {
                throw new Exception("leak handles");
            }
        }

        [Test]
        public void Common()
        {
            Assert.AreEqual("EPERM", Marshal.PtrToStringAnsi(Api.uv_err_name(-1)));
            Assert.AreEqual("operation not permitted", Marshal.PtrToStringAnsi(Api.uv_strerror(-1)));
            Assert.AreEqual(-1, Api.uv_translate_sys_error(1));
        }

        [Test]
        public void Version()
        {
            Assert.AreEqual(71168, Api.uv_version());
            Assert.AreEqual("1.22.0", Marshal.PtrToStringAnsi(Api.uv_version_string()));
        }
        [Test]
        [Repeat(100)]
        public void Loop()
        {
            IntPtr loop = Marshal.AllocCoTaskMem(Api.uv_loop_size());
            Assert.AreEqual(0, Api.uv_loop_init(loop));
            Assert.AreEqual(-22, Api.uv_loop_configure(loop, LoopOption.BlockSignal, 0));
            Assert.AreEqual(0, Api.uv_loop_close(loop));
            Marshal.FreeCoTaskMem(loop);

            Assert.AreNotEqual(IntPtr.Zero, Api.uv_default_loop());
            loop = Api.uv_default_loop();
            Assert.AreEqual(0, Api.uv_run(loop, RunMode.Default));
            Assert.AreEqual(0, Api.uv_loop_alive(loop));
            Api.uv_stop(loop);
            Assert.AreEqual(1072, Api.uv_loop_size());
            Assert.AreNotEqual(0, Api.uv_backend_fd(loop));
            Assert.AreEqual(0, Api.uv_backend_timeout(loop));
            Assert.AreNotEqual(0, Api.uv_now(loop));
            Api.uv_update_time(loop);
            Api.uv_walk(loop, (handle, arg) => { Assert.AreEqual(IntPtr.Zero, arg); }, IntPtr.Zero);
            //Assert.AreEqual(0, Api.uv_loop_fork(loop));
            Assert.AreEqual(IntPtr.Zero, Api.uv_loop_get_data(loop));
            Assert.AreEqual(IntPtr.Zero, Api.uv_loop_set_data(loop, IntPtr.Zero));
            Assert.AreEqual(0, Api.uv_loop_close(loop));
        }

        [Test]
        [Repeat(100)]
        public void Timer()
        {
            Assert.AreEqual(13, (int)HandleType.Timer);
            Assert.AreEqual(152, Api.uv_handle_size(HandleType.Timer));

            IntPtr loop = Marshal.AllocCoTaskMem(Api.uv_loop_size());
            Assert.AreEqual(0, Api.uv_loop_init(loop));
            var timer = Marshal.AllocCoTaskMem(Api.uv_handle_size(HandleType.Timer));
            Assert.AreEqual(0, Api.uv_timer_init(loop, timer));

            bool called = false;
            Assert.AreEqual(0, Api.uv_timer_start(timer, t =>
            {
                called = true;
                Assert.AreEqual(timer, t);
                Api.uv_close(timer, null);
            }, 0, 0));
            Assert.AreEqual(0, Api.uv_run(loop, RunMode.Default));

            Assert.AreEqual(true, called);

            Marshal.FreeCoTaskMem(timer);
            Assert.AreEqual(0, Api.uv_loop_close(loop));
            Marshal.FreeCoTaskMem(loop);
        }

        [Test]
        [Repeat(100)]
        public void TCP()
        {
            IntPtr loop = Api.uv_default_loop();
            IntPtr handle = Marshal.AllocCoTaskMem(Api.uv_handle_size(HandleType.Tcp));
            Assert.AreEqual(0, Api.uv_tcp_init(loop, handle));

            IntPtr addr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Api.SockaddrIn)));
            Assert.AreEqual(0, Api.uv_ip4_addr("0.0.0.0", 8888, addr));
            Assert.AreEqual(0, Api.uv_tcp_bind(handle, addr, 0));
            Marshal.FreeCoTaskMem(addr);

            {
                IntPtr sockaddrs = Marshal.AllocCoTaskMem(0);
                int socksize = 0;
                Assert.AreEqual(0, Api.uv_tcp_getsockname(handle, sockaddrs, ref socksize));
                Marshal.FreeCoTaskMem(sockaddrs);
            }

            {
                int socksize = Marshal.SizeOf(typeof(Api.Sockaddr)) * 2;
                IntPtr sockaddrs = Marshal.AllocCoTaskMem(socksize);
                Assert.AreEqual(32, socksize);
                Assert.AreEqual(0, Api.uv_tcp_getsockname(handle, sockaddrs, ref socksize));
                Assert.AreEqual(16, socksize);
                //var sockaddr = (Api.Sockaddr)Marshal.PtrToStructure(sockaddrs, typeof(Api.Sockaddr));
                Marshal.FreeCoTaskMem(sockaddrs);
            }

            Api.uv_close(handle, null);
            Assert.AreEqual(0, Api.uv_run(loop, RunMode.Default));

            Marshal.FreeCoTaskMem(handle);
        }

        [Test]
        [Repeat(100)]
        public void DNS()
        {
            IntPtr loop = Api.uv_default_loop();
            IntPtr req = Marshal.AllocCoTaskMem(Api.uv_req_size(ReqType.Getaddrinfo));
            Api.Addrinfo hints = new Api.Addrinfo();
            hints.ai_family = /*AF_INET*/2;
            hints.ai_socktype = /*SOCK_STREAM*/1;
            hints.ai_flags = /*AI_CANONNAME*/2;

            bool called = false;
            Assert.AreEqual(0, Api.uv_getaddrinfo(loop, req, (_, status, res) =>
            {
                called = true;
                Api.Addrinfo ai = (Api.Addrinfo)Marshal.PtrToStructure(res, typeof(Api.Addrinfo));
                //Debug.LogFormat("addrlen: {0}", ai.ai_addrlen);
                //Debug.LogFormat("family: {0}", ai.ai_family);
                //Debug.LogFormat("flags: {0}", ai.ai_flags);
                //Debug.LogFormat("protocol: {0}", ai.ai_protocol);
                //Debug.LogFormat("socktype: {0}", ai.ai_socktype);
                //Debug.LogFormat("ai_canonname: {0}", Marshal.PtrToStringAnsi(ai.ai_canonname));
                Assert.AreEqual(0, status);
                Api.uv_freeaddrinfo(res);
            }, "example.com", "80", ref hints));

            Assert.AreEqual(0, Api.uv_run(loop, RunMode.Default));

            Assert.AreEqual(true, called);

            Marshal.FreeCoTaskMem(req);
            Assert.AreEqual(0, Api.uv_loop_close(loop));
        }
    }
}