using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVTimer : UVHandle
    {
        public UVTimer(UVLoop loop) : base(HandleType.Timer, ptr =>
        {
            Ensure.Success(Api.uv_timer_init(loop.NativeHandle, ptr));
        })
        {
        }

        public void Start(Action cb, long timeout, long repeat)
        {
            checked
            {
                Ensure.Success(Api.uv_timer_start(handle, _ => { cb(); }, (ulong)timeout, (ulong)repeat));
            }
        }
        public void Stop()
        {
            Ensure.Success(Api.uv_timer_stop(handle));
        }
        public void Again()
        {
            Ensure.Success(Api.uv_timer_again(handle));
        }
        public long Repeat
        {
            get
            {
                return checked((long)Api.uv_timer_get_repeat(handle));
            }
            set
            {
                Api.uv_timer_set_repeat(handle, checked((ulong)value));
            }
        }
    }
}