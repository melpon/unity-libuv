using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVCheck : UVHandle
    {
        public UVCheck(UVLoop loop) : base(HandleType.Check, ptr =>
        {
            Ensure.Success(Api.uv_check_init(loop.NativeHandle, ptr));
        })
        {
        }

        public void Start(Action cb)
        {
            Ensure.Success(Api.uv_check_start(this.handle, _ => { cb(); }));
        }
        public void Stop()
        {
            Ensure.Success(Api.uv_check_stop(handle));
        }
    }
}