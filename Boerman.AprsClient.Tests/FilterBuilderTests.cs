using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Boerman.AprsClient.Tests
{
    [TestClass]
    public class FilterBuilderTests
    {
        [TestMethod]
        public void TestRangeFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Range(52, 5, 10));
            Assert.AreEqual("r/52/5/10", builder.GetFilter());
        }

        [TestMethod]
        public void TestPrefixFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Prefix("DD", "DE"));
            Assert.AreEqual("p/DD/DE", builder.GetFilter());
        }

        [TestMethod]
        public void TestBudlistFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Budlist("abcdefg"));
            Assert.AreEqual("b/abcdefg", builder.GetFilter());
        }

        [TestMethod]
        public void TestObjectFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Object("object"));
            Assert.AreEqual("o/object", builder.GetFilter());
        }

        [TestMethod]
        public void TestTypeFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Type(AprsFilter.Types.All));
            Assert.AreEqual("t/poimqstunw", builder.GetFilter());
        }

        [TestMethod]
        public void TestSymbolFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Symbol("!", "@", "3"));
            Assert.AreEqual("s/!/@/3", builder.GetFilter());
        }

        [TestMethod]
        public void TestDigipeaterFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Digipeater("TEST1", "TEST2"));
            Assert.AreEqual("d/TEST1/TEST2", builder.GetFilter());
        }

        [TestMethod]
        public void TestAreaFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Area(1, 2, 3, 4));
            Assert.AreEqual("a/1/2/3/4", builder.GetFilter());
        }

        [TestMethod]
        public void TestEntryStationFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.EntryStation("test"));
            Assert.AreEqual("e/test", builder.GetFilter());
        }

        [TestMethod]
        public void TestGroupMessageFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.GroupMessage("test"));
            Assert.AreEqual("g/test", builder.GetFilter());
        }

        [TestMethod]
        public void TestUnprotoFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.Unproto("test"));
            Assert.AreEqual("u/test", builder.GetFilter());
        }

        [TestMethod]
        public void TestMyRangeFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.MyRange(100));
            Assert.AreEqual("m/100", builder.GetFilter());
        }

        [TestMethod]
        public void TestFriendRangeFilter() {
            var builder = new AprsFilterBuilder();
            builder.AddFilter(new AprsFilter.FriendRange("TEST", 100));
            Assert.AreEqual("f/TEST/100", builder.GetFilter());
        }
    }
}
