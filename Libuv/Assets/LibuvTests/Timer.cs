using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using Assert = UnityEngine.Assertions.Assert;

namespace Libuv
{
    public class TimerTest
    {
        [TearDown]
        public void TearDown()
        {
            Util.TearDown();
        }

        [Test]
        [Repeat(100)]
        public void Timer1()
        {
            using (var timer = new UVTimer(UVLoop.Default))
            {
                bool called = false;
                timer.Start(() =>
                {
                    called = true;
                    timer.Close(null);
                }, 0, 0);
                Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
                Assert.IsTrue(called);
            }

            UVLoop.Default.Close();
        }

        [Test]
        [Repeat(100)]
        public void Timer2()
        {
            using (var timer = new UVTimer(UVLoop.Default))
            {
                bool called = false;
                timer.Start(() =>
                {
                    called = true;
                    timer.Stop();
                }, 0, 1);
                Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
                Assert.IsTrue(called);
            }

            try
            {
                UVLoop.Default.Close();
                Assert.IsTrue(false);
            }
            catch (UVError error)
            {
                Assert.AreEqual("UVError: resource busy or locked (EBUSY)", error.Message);
            }
            Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
            UVLoop.Default.Close();
        }

        [Test]
        [Repeat(100)]
        public void Timer3()
        {
            using (var timer = new UVTimer(UVLoop.Default))
            {
                bool called = false;
                timer.Start(() =>
                {
                    called = true;
                    UVLoop.Default.Stop();
                }, 0, 1);
                Assert.IsFalse(UVLoop.Default.Run(RunMode.Default));
                Assert.IsTrue(called);
            }

            Assert.IsTrue(UVLoop.Default.Run(RunMode.Default));
            UVLoop.Default.Close();
        }
    }
}