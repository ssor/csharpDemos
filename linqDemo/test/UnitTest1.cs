using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using linqDemo;


namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<string> list = new List<string> { "10001-aaaa.dav", "10001-bbbb.dav", "10002-cccc.dav" };

            var names = Program.GetDistinctName(list);

            Assert.IsTrue(names.IndexOf("10001-bbbb.dav") >= 0);
            Assert.IsTrue(names.IndexOf("10002-cccc.dav") >= 0);
        }
    }
}
