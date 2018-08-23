using System;
using System.Runtime.InteropServices;

namespace Libuv
{
    public class UVHandle : IDisposable
    {
        protected IntPtr handle;
        public IntPtr NativeHandle
        {
            get { return this.handle; }
        }

        public UVHandle()
        {
            this.handle = IntPtr.Zero;
        }

        public UVHandle(IntPtr handle)
        {
            this.handle = handle;
        }
        public UVHandle(HandleType type, Action<IntPtr> init)
        {
            this.handle = Marshal.AllocCoTaskMem(Api.uv_handle_size(type));
            init(this.handle);
        }

        public T MoveFrom<T>(T rhs) where T : UVHandle
        {
            Close(null);
            this.handle = rhs.handle;
            rhs.handle = IntPtr.Zero;
            return (T)this;
        }

        public override int GetHashCode()
        {
            return handle.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return handle.Equals(obj);
        }

        public static bool operator ==(UVHandle a, UVHandle b)
        {
            if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null)) return true;
            if (object.ReferenceEquals(a, null) && !object.ReferenceEquals(b, null)) return false;
            if (!object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null)) return false;
            return a.handle == b.handle;
        }

        public static bool operator !=(UVHandle a, UVHandle b)
        {
            if (object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null)) return false;
            if (object.ReferenceEquals(a, null) && !object.ReferenceEquals(b, null)) return true;
            if (!object.ReferenceEquals(a, null) && object.ReferenceEquals(b, null)) return true;
            return a.handle != b.handle;
        }

        public bool IsActive
        {
            get
            {
                return Api.uv_is_active(handle) != 0;
            }
        }
        public bool IsClosing
        {
            get
            {
                return Api.uv_is_closing(handle) != 0;
            }
        }
        public void Close(Action cb)
        {
            if (handle == IntPtr.Zero) return;

            Api.uv_close(handle, ptr =>
            {
                if (cb != null)
                {
                    cb();
                }
                Marshal.FreeCoTaskMem(ptr);
            });
            handle = IntPtr.Zero;
        }

        public void Ref()
        {
            Api.uv_ref(handle);
        }
        public void Unref()
        {
            Api.uv_unref(handle);
        }
        public bool HasRef
        {
            get
            {
                return Api.uv_has_ref(handle) != 0;
            }
        }
        //extern public static int uv_handle_size(HandleType type);
        public int SendBufferSize
        {
            get
            {
                int value = 0;
                Ensure.Success(Api.uv_send_buffer_size(handle, ref value));
                return value;
            }
        }
        public int RecvBufferSize
        {
            get
            {
                int value = 0;
                Ensure.Success(Api.uv_recv_buffer_size(handle, ref value));
                return value;
            }
        }
        // extern public static int uv_fileno(IntPtr handle, ref /*uv_os_fd_t*/long fd);
        public UVLoop Loop
        {
            get
            {
                return new UVLoop(Api.uv_handle_get_loop(handle));
            }
        }
        // extern public static IntPtr uv_handle_get_data(IntPtr handle);
        // extern public static IntPtr uv_handle_set_data(IntPtr handle, IntPtr data);
        public HandleType HandleType
        {
            get
            {
                return Api.uv_handle_get_type(handle);
            }
        }
        public static string GetHandleTypeName(HandleType type)
        {
            var ptr = Api.uv_handle_type_name(type);
            return Marshal.PtrToStringAnsi(ptr);
        }

        public void Dispose()
        {
            Close(null);
        }

    }

}