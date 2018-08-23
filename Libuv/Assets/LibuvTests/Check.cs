using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Libuv
{
    public class CheckTest
    {
        [TearDown]
        public void TearDown()
        {
            Util.TearDown();
        }

        [Test]
        [Repeat(100)]
        public void Check1()
        {
            using (var check = new UVCheck(UVLoop.Default))
            {
                bool called = false;
                check.Start(() =>
                {
                    called = true;
                    check.Close(null);
                });
                Assert.IsTrue(UVLoop.Default.Run(RunMode.Nowait));
                Assert.IsTrue(called);
            }

            UVLoop.Default.Close();
        }
    }
}