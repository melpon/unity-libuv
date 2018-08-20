using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class Handler : UnityEngine.MonoBehaviour
{
    void Start()
    {
        // もし残ってたらヤバいので強制的に閉じる
        Libuv.Api.uv_walk(Libuv.Api.uv_default_loop(), (handle, arg) =>
        {
            Libuv.Api.uv_close(handle, null);
        }, IntPtr.Zero);
        Assert(0, Libuv.Api.uv_run(Libuv.Api.uv_default_loop(), Libuv.Api.RunMode.Default));
        Assert(0, Libuv.Api.uv_loop_close(Libuv.Api.uv_default_loop()));

        Test();
        UnityEngine.Debug.Log("finished");
    }

    void Assert<T>(T a, T b)
    {
        if (!object.Equals(a, b))
        {
            throw new Exception(string.Format("expected: {0}, actual: {1}", a, b));
        }
    }

    void AssertNot<T>(T a, T b)
    {
        if (object.Equals(a, b))
        {
            throw new Exception(string.Format("expected: {0}, actual: {1}", a, b));
        }
    }

    void Test()
    {
        for (int i = 0; i < 10000; i++)
        {
            TestApi();
            Assert(0, Libuv.Api.uv_loop_close(Libuv.Api.uv_default_loop()));
        }
    }
    void TestApi()
    {
        TestApiCommon();
        TestApiVersion();
        TestApiLoop();
        TestApiTimer();
    }
    void TestApiCommon()
    {
        Assert("EPERM", Marshal.PtrToStringAnsi(Libuv.Api.uv_err_name(-1)));
        Assert("operation not permitted", Marshal.PtrToStringAnsi(Libuv.Api.uv_strerror(-1)));
        Assert(-1, Libuv.Api.uv_translate_sys_error(1));
    }
    void TestApiVersion()
    {
        Assert(71168, Libuv.Api.uv_version());
        Assert("1.22.0", Marshal.PtrToStringAnsi(Libuv.Api.uv_version_string()));
    }
    void TestApiLoop()
    {
        IntPtr loop = Marshal.AllocCoTaskMem(Libuv.Api.uv_loop_size());
        Assert(0, Libuv.Api.uv_loop_init(loop));
        Assert(-22, Libuv.Api.uv_loop_configure(loop, Libuv.Api.LoopOption.BlockSignal, 0));
        Assert(0, Libuv.Api.uv_loop_close(loop));
        Marshal.FreeCoTaskMem(loop);

        AssertNot(IntPtr.Zero, Libuv.Api.uv_default_loop());
        loop = Libuv.Api.uv_default_loop();
        Assert(0, Libuv.Api.uv_run(loop, Libuv.Api.RunMode.Default));
        Assert(0, Libuv.Api.uv_loop_alive(loop));
        Libuv.Api.uv_stop(loop);
        Assert(1072, Libuv.Api.uv_loop_size());
        AssertNot(0, Libuv.Api.uv_backend_fd(loop));
        Assert(0, Libuv.Api.uv_backend_timeout(loop));
        AssertNot(0, Libuv.Api.uv_now(loop));
        Libuv.Api.uv_update_time(loop);
        Libuv.Api.uv_walk(loop, (handle, arg) => { Assert(IntPtr.Zero, arg); }, IntPtr.Zero);
        //Assert(0, Libuv.Api.uv_loop_fork(loop));
        Assert(IntPtr.Zero, Libuv.Api.uv_loop_get_data(loop));
        Assert(IntPtr.Zero, Libuv.Api.uv_loop_set_data(loop, IntPtr.Zero));
    }
    void TestApiTimer()
    {
        Assert(13, (int)Libuv.Api.HandleType.Timer);
        Assert(152, Libuv.Api.uv_handle_size(Libuv.Api.HandleType.Timer));

        IntPtr loop = Marshal.AllocCoTaskMem(Libuv.Api.uv_loop_size());
        Assert(0, Libuv.Api.uv_loop_init(loop));
        var timer = Marshal.AllocCoTaskMem(Libuv.Api.uv_handle_size(Libuv.Api.HandleType.Timer));
        Assert(0, Libuv.Api.uv_timer_init(loop, timer));

        bool called = false;
        Assert(0, Libuv.Api.uv_timer_start(timer, t =>
        {
            called = true;
            Assert(timer, t);
            Libuv.Api.uv_close(timer, null);
        }, 0, 0));
        Assert(0, Libuv.Api.uv_run(loop, Libuv.Api.RunMode.Default));

        Assert(true, called);

        Marshal.FreeCoTaskMem(timer);
        Assert(0, Libuv.Api.uv_loop_close(loop));
        Marshal.FreeCoTaskMem(loop);
    }
}