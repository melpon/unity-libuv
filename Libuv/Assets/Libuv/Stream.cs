using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVStream : UVHandle
    {
        public void Shutdown(Action<UVError> shutdownCb)
        {
            var req = Marshal.AllocCoTaskMem(Api.uv_req_size(ReqType.Shutdown));
            Ensure.Success(Api.uv_shutdown(req, handle, (_, status) =>
            {
                if (shutdownCb != null)
                {
                    shutdownCb(UVError.FromStatus(status));
                }
                Marshal.FreeCoTaskMem(req);
            }));
        }
        public void Listen(int backlog, Action<UVError> connectionCb)
        {
            Ensure.Success(Api.uv_listen(handle, backlog, (_, status) =>
            {
                connectionCb(UVError.FromStatus(status));
            }));
        }
    }
}