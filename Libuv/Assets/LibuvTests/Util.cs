using System;

namespace Libuv
{
    public class Util
    {
        public static void TearDown()
        {
            bool leak = false;
            UVLoop.Default.Walk(handle =>
            {
                leak = true;
                handle.Close(null);
            });
            UVLoop.Default.Run(RunMode.Default);
            UVLoop.Default.Close();

            if (leak)
            {
                throw new Exception("leak handles");
            }
        }
    }
}