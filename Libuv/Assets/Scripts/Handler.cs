using System;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Handler : MonoBehaviour
{

    [DllImport("libuv")]
    extern private static IntPtr uv_loop_new();
    [DllImport("libuv")]
    extern private static void uv_loop_delete(IntPtr loop);
    [DllImport("libuv")]
    extern private static int uv_run(IntPtr p, int v);

    // Use this for initialization
    void Start()
    {
        var p = uv_loop_new();
        uv_loop_delete(p);
    }
}
