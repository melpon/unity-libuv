using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Libuv
{
    public class IdleTest
    {
        [TearDown]
        public void TearDown()
        {
            Util.TearDown();
        }

        [Test]
        [Repeat(100)]
        public void Idle1()
        {
            using (var idle = new UVIdle(UVLoop.Default))
            {
                bool called = false;
                idle.Start(() =>
                {
                    called = true;
                    idle.Close(null);
                });
                Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
                Assert.IsTrue(called);
            }

            UVLoop.Default.Close();
        }
    }
}