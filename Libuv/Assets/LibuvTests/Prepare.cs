using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Libuv
{
    public class PrepareTest
    {
        [TearDown]
        public void TearDown()
        {
            Util.TearDown();
        }

        [Test]
        [Repeat(100)]
        public void Prepare1()
        {
            using (var prepare = new UVPrepare(UVLoop.Default))
            {
                bool called = false;
                prepare.Start(() =>
                {
                    called = true;
                    prepare.Close(null);
                });
                Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
                Assert.IsTrue(called);
            }

            UVLoop.Default.Close();
        }
    }
}