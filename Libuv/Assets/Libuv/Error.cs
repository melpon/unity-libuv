using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVError : Exception
    {
        public UVError(int error) : base(ToMessage(error))
        {
        }

        private static string ToMessage(int error)
        {
            var strerror = Marshal.PtrToStringAnsi(Api.uv_strerror(error));
            var err_name = Marshal.PtrToStringAnsi(Api.uv_err_name(error));
            return string.Format("UVError: {0} ({1})", strerror, err_name);
        }
        public static UVError FromStatus(int status)
        {
            if (status == 0)
            {
                return null;
            }
            return new UVError(status);
        }
    }

    internal static class Ensure
    {
        public static void Success(int error)
        {
            if (error == 0) return;
            throw new UVError(error);
        }
    }
}