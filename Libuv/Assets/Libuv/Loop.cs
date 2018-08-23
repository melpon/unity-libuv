using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVLoop : IDisposable
    {
        IntPtr loop;
        public IntPtr NativeHandle
        {
            get { return loop; }
        }

        public UVLoop()
        {
            this.loop = IntPtr.Zero;
        }

        public UVLoop(IntPtr loop)
        {
            this.loop = loop;
        }
        public void Init()
        {
            Close();
            loop = Marshal.AllocCoTaskMem(Api.uv_loop_size());
            Ensure.Success(Api.uv_loop_init(loop));
        }

        public UVLoop MoveFrom(UVLoop rhs)
        {
            this.loop = rhs.loop;
            rhs.loop = IntPtr.Zero;
            return this;
        }

        public override int GetHashCode()
        {
            return loop.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return loop.Equals(obj);
        }

        public static bool operator ==(UVLoop a, UVLoop b)
        {
            return a.loop == b.loop;
        }

        public static bool operator !=(UVLoop a, UVLoop b)
        {
            return a.loop == b.loop;
        }

        public void Configure(LoopOption option, int arg)
        {
            Ensure.Success(Api.uv_loop_configure(loop, option, arg));
        }
        public void Close()
        {
            if (loop == IntPtr.Zero) return;

            Ensure.Success(Api.uv_loop_close(loop));
            if (loop != Default.NativeHandle)
            {
                Marshal.FreeCoTaskMem(loop);
            }
            loop = IntPtr.Zero;
        }
        public static UVLoop Default
        {
            get
            {
                return new UVLoop(Api.uv_default_loop());
            }
        }
        public bool Run(RunMode mode)
        {
            return Api.uv_run(loop, mode) == 0;
        }
        public bool Alive
        {
            get
            {
                return Api.uv_loop_alive(loop) != 0;
            }
        }
        public void Stop()
        {
            Api.uv_stop(loop);
        }
        public ulong Now
        {
            get
            {
                return Api.uv_now(loop);
            }
        }
        public void UpdateTime()
        {
            Api.uv_update_time(loop);
        }
        public void Walk(Action<UVHandle> cb)
        {
            Api.uv_walk(loop, (handle, _) =>
            {
                cb(new UVHandle(handle));
            }, IntPtr.Zero);
        }

        public void Dispose()
        {
            try
            {
                Close();
            }
            catch (Exception) { }
        }
    }
}