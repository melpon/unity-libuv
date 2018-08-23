namespace Libuv
{
    public enum RunMode
    {
        Default = 0,
        Once,
        Nowait,
    }
    public enum LoopOption
    {
        BlockSignal = 0
    }

    public enum HandleType
    {
        UnknownHandle = 0,
        Async,
        Check,
        FsEvent,
        FsPoll,
        Handle,
        Idle,
        NamedPipe,
        Poll,
        Prepare,
        Process,
        Stream,
        Tcp,
        Timer,
        Tty,
        Udp,
        Signal,
        File,
    }

    public enum ReqType
    {
        UnknownReq,
        Req,
        Connect,
        Write,
        Shutdown,
        UdpSend,
        Fs,
        Work,
        Getaddrinfo,
        Getnameinfo,
        Private,
    }

    public enum PollEvent
    {
        Readable = 1,
        Writable = 2,
        Disconnect = 4,
        Prioritized = 8
    }

    public enum Signal
    {
        // Win, Unix
        SIGHUP = 1,
        // Win, Unix
        SIGINT = 2,
        // Unix
        SIGQUIT = 3,
        // Win, Unix
        SIGILL = 4,
        // Win, Unix
        SIGABRT = 6,
        // Win, Unix
        SIGFPE = 8,
        // Win, Unix
        SIGKILL = 9,
        // Win, Unix
        SIGSEGV = 11,
        // Unix
        SIGPIPE = 13,
        // Unix
        SIGALRM = 14,
        // Win, Unix
        SIGTERM = 15,
        // Win
        SIGBREAK = 21,
        // Win
        SIGWINCH = 28,
    }

    public enum TcpFlags
    {
        TcpIpv6only = 1
    }

    public enum TTYMode
    {
        Normal,
        Raw,
        IO,
    }

    public enum UdpFlags
    {
        UdpIpv6only = 1,
        UdpPartial = 2,
        UdpReuseaddr = 4,
    }
    public enum Membership
    {
        LeaveGroup = 0,
        JoinGroup,
    }

    public enum FsEventFlags
    {
        FsEventWatchEntry = 1,
        FsEventStat = 2,
        FsEventRecursive = 4,
    };
    public enum FsEvent
    {
        Rename = 1,
        Change = 2
    };

}