using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class Api
    {
        // commons

        [DllImport("libuv")]
        extern public static IntPtr uv_strerror(int err);

        [DllImport("libuv")]
        extern public static IntPtr uv_err_name(int err);

        [DllImport("libuv")]
        extern public static int uv_translate_sys_error(int sys_errno);

        // versions

        [DllImport("libuv")]
        extern public static int uv_version();

        [DllImport("libuv")]
        extern public static IntPtr uv_version_string();

        // loops

        public enum RunMode
        {
            Default = 0,
            Once,
            Nowait,
        }
        public enum LoopOption
        {
            BlockSignal = 0
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void WalkCb(IntPtr handle, IntPtr arg);

        [DllImport("libuv")]
        extern public static int uv_loop_init(IntPtr loop);
        [DllImport("libuv")]
        extern public static int uv_loop_configure(IntPtr loop, LoopOption option, int arg);
        [DllImport("libuv")]
        extern public static int uv_loop_close(IntPtr loop);
        [DllImport("libuv")]
        extern public static IntPtr uv_default_loop();
        [DllImport("libuv")]
        extern public static int uv_run(IntPtr loop, RunMode mode);
        [DllImport("libuv")]
        extern public static int uv_loop_alive(IntPtr loop);
        [DllImport("libuv")]
        extern public static void uv_stop(IntPtr loop);
        [DllImport("libuv")]
        extern public static int uv_loop_size();
        [DllImport("libuv")]
        extern public static int uv_backend_fd(IntPtr loop);
        [DllImport("libuv")]
        extern public static int uv_backend_timeout(IntPtr loop);
        [DllImport("libuv")]
        extern public static long uv_now(IntPtr loop);
        [DllImport("libuv")]
        extern public static void uv_update_time(IntPtr loop);
        [DllImport("libuv", CallingConvention = CallingConvention.Cdecl)]
        extern public static void uv_walk(IntPtr loop, WalkCb walkCb, IntPtr arg);
        [DllImport("libuv")]
        extern public static int uv_loop_fork(IntPtr loop);
        [DllImport("libuv")]
        extern public static IntPtr uv_loop_get_data(IntPtr loop);
        [DllImport("libuv")]
        extern public static IntPtr uv_loop_set_data(IntPtr loop, IntPtr data);

        // handles
        public enum HandleType
        {
            UnknownHandle = 0,
            Async,
            Check,
            FsEvent,
            FsPoll,
            Handle,
            Idle,
            NamedPipe,
            Poll,
            Prepare,
            Process,
            Stream,
            Tcp,
            Timer,
            Tty,
            Udp,
            Signal,
            File,
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AllocCb(IntPtr handle, int suggested_size, ref Buf buf);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CloseCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_is_active(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_is_closing(IntPtr handle);
        [DllImport("libuv")]
        extern public static void uv_close(IntPtr handle, CloseCb closeCb);
        [DllImport("libuv")]
        extern public static void uv_ref(IntPtr handle);
        [DllImport("libuv")]
        extern public static void uv_unref(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_has_ref(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_handle_size(HandleType type);
        [DllImport("libuv")]
        extern public static int uv_send_buffer_size(IntPtr handle, ref int value);
        [DllImport("libuv")]
        extern public static int uv_recv_buffer_size(IntPtr handle, ref int value);
        [DllImport("libuv")]
        extern public static int uv_fileno(IntPtr handle, ref /*uv_os_fd_t*/long fd);
        [DllImport("libuv")]
        extern public static IntPtr uv_handle_get_loop(IntPtr handle);
        [DllImport("libuv")]
        extern public static IntPtr uv_handle_get_data(IntPtr handle);
        [DllImport("libuv")]
        extern public static IntPtr uv_handle_set_data(IntPtr handle, IntPtr data);
        [DllImport("libuv")]
        extern public static HandleType uv_handle_get_type(IntPtr handle);
        [DllImport("libuv")]
        extern public static IntPtr uv_handle_type_name(HandleType type);

        // Base request

        public enum ReqType
        {
            UnknownReq,
            Req,
            Connect,
            Write,
            Shutdown,
            UdpSend,
            Fs,
            Work,
            Getaddrinfo,
            Getnameinfo,
            Private,
        }

        [DllImport("libuv")]
        extern public static int uv_cancel(IntPtr req);
        [DllImport("libuv")]
        extern public static int uv_req_size(ReqType type);
        [DllImport("libuv")]
        extern public static IntPtr uv_req_get_data(IntPtr req);
        [DllImport("libuv")]
        extern public static IntPtr uv_req_set_data(IntPtr req, IntPtr data);
        [DllImport("libuv")]
        extern public static ReqType uv_req_get_type(IntPtr req);
        [DllImport("libuv")]
        extern public static IntPtr uv_req_type_name(ReqType type);

        // Timer handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void TimerCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_timer_init(IntPtr loop, IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_timer_start(IntPtr handle, TimerCb cb, long timeout, long repeat);
        [DllImport("libuv")]
        extern public static int uv_timer_stop(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_timer_again(IntPtr handle);
        [DllImport("libuv")]
        extern public static void uv_timer_set_repeat(IntPtr handle, long repeat);
        [DllImport("libuv")]
        extern public static long uv_timer_get_repeat(IntPtr handle);

        // Prepare handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PrepareCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_prepare_init(IntPtr loop, IntPtr prepare);
        [DllImport("libuv")]
        extern public static int uv_prepare_start(IntPtr prepare, PrepareCb cb);
        [DllImport("libuv")]
        extern public static int uv_prepare_stop(IntPtr prepare);

        // Check handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void CheckCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_check_init(IntPtr loop, IntPtr check);
        [DllImport("libuv")]
        extern public static int uv_check_start(IntPtr check, CheckCb cb);
        [DllImport("libuv")]
        extern public static int uv_check_stop(IntPtr check);

        // Idle handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void IdleCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_idle_init(IntPtr loop, IntPtr idle);
        [DllImport("libuv")]
        extern public static int uv_idle_start(IntPtr idle, IdleCb cb);
        [DllImport("libuv")]
        extern public static int uv_idle_stop(IntPtr idle);

        // Async handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void AsyncCb(IntPtr handle);

        [DllImport("libuv")]
        extern public static int uv_async_init(IntPtr loop, IntPtr async, AsyncCb cb);
        [DllImport("libuv")]
        extern public static int uv_async_send(IntPtr async);

        // Poll handle

        public enum PollEvent
        {
            Readable = 1,
            Writable = 2,
            Disconnect = 4,
            Prioritized = 8
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PollCb(IntPtr handle, int status, PollEvent events);

        [DllImport("libuv")]
        extern public static int uv_poll_init(IntPtr loop, IntPtr poll, int fd);
        [DllImport("libuv")]
        extern public static int uv_poll_init_socket(IntPtr loop, IntPtr poll, /*uv_os_sock_t*/long socket);
        [DllImport("libuv")]
        extern public static int uv_poll_start(IntPtr poll, int events, PollCb cb);
        [DllImport("libuv")]
        extern public static int uv_poll_stop(IntPtr poll);

        // Signal handle

        public enum Signal
        {
            // Win, Unix
            SIGHUP = 1,
            // Win, Unix
            SIGINT = 2,
            // Unix
            SIGQUIT = 3,
            // Win, Unix
            SIGILL = 4,
            // Win, Unix
            SIGABRT = 6,
            // Win, Unix
            SIGFPE = 8,
            // Win, Unix
            SIGKILL = 9,
            // Win, Unix
            SIGSEGV = 11,
            // Unix
            SIGPIPE = 13,
            // Unix
            SIGALRM = 14,
            // Win, Unix
            SIGTERM = 15,
            // Win
            SIGBREAK = 21,
            // Win
            SIGWINCH = 28,
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void SignalCb(IntPtr handle, Signal signum);

        [DllImport("libuv")]
        extern public static int uv_signal_init(IntPtr loop, IntPtr signal);
        [DllImport("libuv")]
        extern public static int uv_signal_start(IntPtr signal, SignalCb cb, Signal signum);
        [DllImport("libuv")]
        extern public static int uv_signal_start_oneshot(IntPtr signal, SignalCb cb, Signal signum);
        [DllImport("libuv")]
        extern public static int uv_signal_stop(IntPtr signal);

        // Process handle

        [DllImport("libuv")]
        extern public static void uv_disable_stdio_inheritance();
        //[DllImport("libuv")]
        //extern public static int uv_spawn(IntPtr loop, IntPtr handle, const uv_process_options_t* options);
        [DllImport("libuv")]
        extern public static int uv_process_kill(IntPtr handle, Signal signum);
        [DllImport("libuv")]
        extern public static int uv_kill(int pid, Signal signum);
        [DllImport("libuv")]
        extern public static /*uv_pid_t*/int uv_process_get_pid(IntPtr handle);

        // Stream handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ReadCb(IntPtr stream, int nread, ref Buf buf);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void WriteCb(IntPtr req, int status);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ConnectCb(IntPtr req, int status);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ShutdownCb(IntPtr req, int status);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void ConnectionCb(IntPtr server, int status);

        [DllImport("libuv")]
        extern public static int uv_shutdown(IntPtr req, IntPtr handle, ShutdownCb cb);
        [DllImport("libuv")]
        extern public static int uv_listen(IntPtr stream, int backlog, ConnectionCb cb);
        [DllImport("libuv")]
        extern public static int uv_accept(IntPtr server, IntPtr client);
        [DllImport("libuv")]
        extern public static int uv_read_start(IntPtr stream, AllocCb allocCb, ReadCb readCb);
        [DllImport("libuv")]
        extern public static int uv_read_stop(IntPtr stream);
        [DllImport("libuv")]
        extern public static int uv_write(IntPtr req, IntPtr handle, [In] Buf[] bufs, uint nbufs, WriteCb cb);
        [DllImport("libuv")]
        extern public static int uv_write2(IntPtr req, IntPtr handle, [In] Buf[] bufs, uint nbufs, IntPtr sendHandle, WriteCb cb);
        [DllImport("libuv")]
        extern public static int uv_try_write(IntPtr handle, [In] Buf[] bufs, uint nbufs);
        [DllImport("libuv")]
        extern public static int uv_is_writable(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_stream_set_blocking(IntPtr handle, int blocking);
        [DllImport("libuv")]
        extern public static long uv_stream_get_write_queue_size(IntPtr stream);

        // TCP handle

        public enum TcpFlags
        {
            TcpIpv6only = 1
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Sockaddr
        {
            public ushort sa_family;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] sa_data;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SockaddrIn
        {
            public short sin_family;
            public ushort sin_port;
            public InAddr sin_addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sin_zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct InAddr
        {
            public byte s_b1, s_b2, s_b3, s_b4;
        }

        [DllImport("libuv")]
        extern public static int uv_tcp_init(IntPtr loop, IntPtr handle);
        // flags -> AF_UNSPEC, AF_INET, AF_INET6
        [DllImport("libuv")]
        extern public static int uv_tcp_init_ex(IntPtr loop, IntPtr handle, int flags);
        [DllImport("libuv")]
        extern public static int uv_tcp_open(IntPtr handle, /*uv_os_sock_t*/long sock);
        [DllImport("libuv")]
        extern public static int uv_tcp_nodelay(IntPtr handle, int enable);
        [DllImport("libuv")]
        extern public static int uv_tcp_keepalive(IntPtr handle, int enable, uint delay);
        [DllImport("libuv")]
        extern public static int uv_tcp_simultaneous_accepts(IntPtr handle, int enable);
        [DllImport("libuv")]
        extern public static int uv_tcp_bind(IntPtr handle, [In] ref Sockaddr addr, TcpFlags flags);
        [DllImport("libuv")]
        extern public static int uv_tcp_getsockname(IntPtr handle, [Out] ref Sockaddr[] name, [In, Out] ref int namelen);
        [DllImport("libuv")]
        extern public static int uv_tcp_getpeername(IntPtr handle, [Out] ref Sockaddr[] name, [In, Out] ref int namelen);
        [DllImport("libuv")]
        extern public static int uv_tcp_connect(IntPtr req, IntPtr handle, [In] ref Sockaddr addr, ConnectCb cb);


        // Miscellaneous utilities

        [StructLayout(LayoutKind.Sequential)]
        public struct Buf
        {
            public IntPtr Base;
            public long Len;
        }
    }
}