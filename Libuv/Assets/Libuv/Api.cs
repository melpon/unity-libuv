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
        extern public static ulong uv_now(IntPtr loop);
        [DllImport("libuv")]
        extern public static void uv_update_time(IntPtr loop);
        [DllImport("libuv")]
        extern public static void uv_walk(IntPtr loop, WalkCb walkCb, IntPtr arg);
        [DllImport("libuv")]
        extern public static int uv_loop_fork(IntPtr loop);
        [DllImport("libuv")]
        extern public static IntPtr uv_loop_get_data(IntPtr loop);
        [DllImport("libuv")]
        extern public static IntPtr uv_loop_set_data(IntPtr loop, IntPtr data);

        // handles
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
        extern public static int uv_timer_start(IntPtr handle, TimerCb cb, ulong timeout, ulong repeat);
        [DllImport("libuv")]
        extern public static int uv_timer_stop(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_timer_again(IntPtr handle);
        [DllImport("libuv")]
        extern public static void uv_timer_set_repeat(IntPtr handle, ulong repeat);
        [DllImport("libuv")]
        extern public static ulong uv_timer_get_repeat(IntPtr handle);

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
        public struct In6Addr
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public byte[] s6_addr;   /* IPv6 address */
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct SockaddrIn6
        {
            public short sin6_family;   /* AF_INET6 */
            public ushort sin6_port;     /* port number */
            public uint sin6_flowinfo; /* IPv6 flow information */
            public In6Addr sin6_addr;     /* IPv6 address */
            public uint sin6_scope_id; /* Scope ID (new in 2.4) */
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
        extern public static int uv_tcp_bind(IntPtr handle, /*[In] ref Sockaddr*/IntPtr addr, TcpFlags flags);
        [DllImport("libuv")]
        extern public static int uv_tcp_getsockname(IntPtr handle, /*[Out] Sockaddr[]*/IntPtr name, [In, Out] ref int namelen);
        [DllImport("libuv")]
        extern public static int uv_tcp_getpeername(IntPtr handle, /*[Out] Sockaddr[]*/IntPtr name, [In, Out] ref int namelen);
        [DllImport("libuv")]
        extern public static int uv_tcp_connect(IntPtr req, IntPtr handle, /*[In] ref Sockaddr*/IntPtr addr, ConnectCb cb);

        // Pipe handle

        [DllImport("libuv")]
        extern public static int uv_pipe_init(IntPtr loop, IntPtr handle, int ipc);
        [DllImport("libuv")]
        extern public static int uv_pipe_open(IntPtr handle, /*uv_file*/int file);
        [DllImport("libuv")]
        extern public static int uv_pipe_bind(IntPtr handle, string name);
        [DllImport("libuv")]
        extern public static void uv_pipe_connect(IntPtr req, IntPtr handle, string name, ConnectCb cb);
        [DllImport("libuv")]
        extern public static int uv_pipe_getsockname(IntPtr handle, [Out] byte[] buffer, [In, Out] ref int size);
        [DllImport("libuv")]
        extern public static int uv_pipe_getpeername(IntPtr handle, [Out] byte[] buffer, [In, Out] ref int size);
        [DllImport("libuv")]
        extern public static void uv_pipe_pending_instances(IntPtr handle, int count);
        [DllImport("libuv")]
        extern public static int uv_pipe_pending_count(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_pipe_chmod(IntPtr handle, PollEvent flags);

        // TTY handle

        [DllImport("libuv")]
        extern public static int uv_tty_init(IntPtr loop, IntPtr handle, /*uv_file*/int fd, int readable);
        [DllImport("libuv")]
        extern public static int uv_tty_set_mode(IntPtr handle, TTYMode mode);
        [DllImport("libuv")]
        extern public static int uv_tty_reset_mode();
        [DllImport("libuv")]
        extern public static int uv_tty_get_winsize(IntPtr handle, out int width, out int height);

        // UDP handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UdpSendCb(IntPtr req, int status);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void UdpRecvCb(IntPtr handle, long nread, IntPtr buf, /*ref Sockaddr*/IntPtr addr, UdpFlags flags);


        [DllImport("libuv")]
        extern public static int uv_udp_init(IntPtr loop, IntPtr handle);
        // flags -> AF_UNSPEC, AF_INET, AF_INET6
        [DllImport("libuv")]
        extern public static int uv_udp_init_ex(IntPtr loop, IntPtr handle, uint flags);
        [DllImport("libuv")]
        extern public static int uv_udp_open(IntPtr handle, /*uv_os_sock_t*/long sock);
        [DllImport("libuv")]
        extern public static int uv_udp_bind(IntPtr handle, /*[In] ref Sockaddr*/IntPtr addr, UdpFlags flags);
        [DllImport("libuv")]
        extern public static int uv_udp_getsockname(IntPtr handle, /*[Out] Sockaddr[]*/IntPtr name, [In, Out] ref int namelen);
        [DllImport("libuv")]
        extern public static int uv_udp_set_membership(IntPtr handle, string multicast_addr, string interface_addr, Membership membership);
        [DllImport("libuv")]
        extern public static int uv_udp_set_multicast_loop(IntPtr handle, int on);
        [DllImport("libuv")]
        extern public static int uv_udp_set_multicast_ttl(IntPtr handle, int ttl);
        [DllImport("libuv")]
        extern public static int uv_udp_set_multicast_interface(IntPtr handle, string interface_addr);
        [DllImport("libuv")]
        extern public static int uv_udp_set_broadcast(IntPtr handle, int on);
        [DllImport("libuv")]
        extern public static int uv_udp_set_ttl(IntPtr handle, int ttl);
        [DllImport("libuv")]
        extern public static int uv_udp_send(IntPtr req, IntPtr handle, [In] Buf[] bufs, uint nbufs, /*[In] ref Sockaddr*/IntPtr addr, UdpSendCb sendCb);
        [DllImport("libuv")]
        extern public static int uv_udp_try_send(IntPtr handle, [In] Buf[] bufs, uint nbufs, /*[In] ref Sockaddr*/IntPtr addr);
        [DllImport("libuv")]
        extern public static int uv_udp_recv_start(IntPtr handle, AllocCb allocCb, UdpRecvCb recvCb);
        [DllImport("libuv")]
        extern public static int uv_udp_recv_stop(IntPtr handle);
        [DllImport("libuv")]
        extern public static long uv_udp_get_send_queue_size(IntPtr handle);
        [DllImport("libuv")]
        extern public static long uv_udp_get_send_queue_count(IntPtr handle);

        // FS Event handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FsEventCb(IntPtr handle, string filename, FsEvent events, int status);

        [DllImport("libuv")]
        extern public static int uv_fs_event_init(IntPtr loop, IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_fs_event_start(IntPtr handle, FsEventCb cb, string path, FsEventFlags flags);
        [DllImport("libuv")]
        extern public static int uv_fs_event_stop(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_fs_event_getpath(IntPtr handle, [Out] byte[] buffer, [In, Out] ref long size);

        // FS Poll handle

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void FsPollCb(IntPtr handle, int status, ref Stat prev, ref Stat curr);

        [DllImport("libuv")]
        extern public static int uv_fs_poll_init(IntPtr loop, IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_fs_poll_start(IntPtr handle, FsPollCb pollCb, string path, uint interval);
        [DllImport("libuv")]
        extern public static int uv_fs_poll_stop(IntPtr handle);
        [DllImport("libuv")]
        extern public static int uv_fs_poll_getpath(IntPtr handle, [Out] byte[] buffer, [In, Out] ref long size);

        // File system operations

        [StructLayout(LayoutKind.Sequential)]
        public struct Timespec
        {
            public long tv_sec;
            public long tv_nsec;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Stat
        {
            public ulong st_dev;
            public ulong st_mode;
            public ulong st_nlink;
            public ulong st_uid;
            public ulong st_gid;
            public ulong st_rdev;
            public ulong st_ino;
            public ulong st_size;
            public ulong st_blksize;
            public ulong st_blocks;
            public ulong st_flags;
            public ulong st_gen;
            public Timespec st_atim;
            public Timespec st_mtim;
            public Timespec st_ctim;
            public Timespec st_birthtim;
        }

        // DNS utility functions
        [StructLayout(LayoutKind.Sequential)]
        public struct Addrinfo
        {
            public int ai_flags;
            public int ai_family;
            public int ai_socktype;
            public int ai_protocol;
            public ulong ai_addrlen;
            public IntPtr ai_canonname;
            public IntPtr ai_addr;
            public IntPtr ai_next;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetaddrinfoCb(IntPtr req, int status, IntPtr res);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void GetnameinfoCb(IntPtr req, int status, string hostname, string service);

        [DllImport("libuv")]
        extern public static int uv_getaddrinfo(IntPtr loop, IntPtr req, GetaddrinfoCb getaddrinfoCb, string node, string service, [In] ref Addrinfo hints);
        [DllImport("libuv")]
        extern public static void uv_freeaddrinfo(IntPtr ai);
        // flags:
        // - NI_NAMEREQD
        // - NI_DGRAM
        // - NI_NOFQDN
        // - NI_NUMERICHOST
        // - NI_NUMERICSERV
        [DllImport("libuv")]
        extern public static int uv_getnameinfo(IntPtr loop, IntPtr req, GetnameinfoCb getnameinfoCb, [In] ref Addrinfo addr, int flags);

        // Shared library handling

        // Threading and synchronization utilities

        // Miscellaneous utilities

        [StructLayout(LayoutKind.Sequential)]
        public struct Buf
        {
            public IntPtr Base;
            public long Len;
        }
        [DllImport("libuv")]
        extern public static int uv_ip4_addr(string ip, int port, /*ref SockaddrIn*/IntPtr addr);
        [DllImport("libuv")]
        extern public static int uv_ip6_addr(string ip, int port, /*ref SockaddrIn6*/IntPtr addr);

    }
}