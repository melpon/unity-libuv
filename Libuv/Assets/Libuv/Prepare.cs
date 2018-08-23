using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVPrepare : UVHandle
    {
        public UVPrepare(UVLoop loop) : base(HandleType.Prepare, ptr =>
        {
            Ensure.Success(Api.uv_prepare_init(loop.NativeHandle, ptr));
        })
        {
        }

        public void Start(Action cb)
        {
            Ensure.Success(Api.uv_prepare_start(this.handle, _ => { cb(); }));
        }
        public void Stop()
        {
            Ensure.Success(Api.uv_prepare_stop(handle));
        }
    }
}