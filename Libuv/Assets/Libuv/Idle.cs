using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVIdle : UVHandle
    {
        public UVIdle(UVLoop loop) : base(HandleType.Idle, ptr =>
        {
            Ensure.Success(Api.uv_idle_init(loop.NativeHandle, ptr));
        })
        {
        }

        public void Start(Action cb)
        {
            Ensure.Success(Api.uv_idle_start(this.handle, _ => { cb(); }));
        }
        public void Stop()
        {
            Ensure.Success(Api.uv_idle_stop(handle));
        }
    }
}