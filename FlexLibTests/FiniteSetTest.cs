using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FlexLib.Sets;

namespace FlexLibTests
{
    [TestClass]
    public class FiniteSetTest
    {
        private FiniteSet<int> TestSetDefaultEmpty;
        private FiniteSet<int> TestSetArrayEmpty;
        private FiniteSet<int> TestSetEnumerableEmpty;
        private FiniteSet<int> TestSetArrayNumbers;
        private FiniteSet<int> TestSetEnumerableNumbers;
        private FiniteSet<string> TestSetArrayStrings;
        private FiniteSet<string> TestSetEnumerableStrings;

        [TestInitialize]
        public void TestInitialize()
        {
            TestSetDefaultEmpty = new FiniteSet<int>();
            TestSetArrayEmpty = new FiniteSet<int>(new int[] { });
            TestSetEnumerableEmpty = new FiniteSet<int>(new List<int> { });

            TestSetArrayNumbers = new FiniteSet<int>(new int[] { 2, 1, 6 });
            TestSetEnumerableNumbers = new FiniteSet<int>(new List<int> { 6, 2, 1 });

            TestSetArrayStrings = new FiniteSet<string>(new string[] { "Apple", "Banana", "Cherry", "Date" });
            TestSetEnumerableStrings = new FiniteSet<string>(new List<string> { "Eggplant", "Fennel", "Garlic" });
        }

        [TestMethod]
        public void TestCount()
        {
            Assert.AreEqual(0, TestSetDefaultEmpty.Count);
            Assert.AreEqual(0, TestSetArrayEmpty.Count);
            Assert.AreEqual(0, TestSetEnumerableEmpty.Count);

            Assert.AreEqual(3, TestSetArrayNumbers.Count);
            Assert.AreEqual(3, TestSetEnumerableNumbers.Count);

            Assert.AreEqual(4, TestSetArrayStrings.Count);
            Assert.AreEqual(3, TestSetEnumerableStrings.Count);
        }

        [TestMethod]
        public void TestElements()
        {
            Assert.IsFalse(TestSetDefaultEmpty.Contains(0));
            Assert.IsFalse(TestSetDefaultEmpty.Contains(23));

            Assert.IsFalse(TestSetArrayEmpty.Contains(0));
            Assert.IsFalse(TestSetArrayEmpty.Contains(23));

            Assert.IsFalse(TestSetEnumerableEmpty.Contains(0));
            Assert.IsFalse(TestSetEnumerableEmpty.Contains(23));

            Assert.IsTrue(TestSetArrayNumbers.Contains(1));
            Assert.IsTrue(TestSetArrayNumbers.Contains(2));
            Assert.IsTrue(TestSetArrayNumbers.Contains(6));
            Assert.IsFalse(TestSetArrayNumbers.Contains(0));
            Assert.IsFalse(TestSetArrayNumbers.Contains(23));

            Assert.IsTrue(TestSetEnumerableNumbers.Contains(1));
            Assert.IsTrue(TestSetEnumerableNumbers.Contains(2));
            Assert.IsTrue(TestSetEnumerableNumbers.Contains(6));
            Assert.IsFalse(TestSetEnumerableNumbers.Contains(0));
            Assert.IsFalse(TestSetEnumerableNumbers.Contains(23));

            Assert.IsTrue(TestSetArrayStrings.Contains("Apple"));
            Assert.IsTrue(TestSetArrayStrings.Contains("Banana"));
            Assert.IsTrue(TestSetArrayStrings.Contains("Cherry"));
            Assert.IsTrue(TestSetArrayStrings.Contains("Date"));
            Assert.IsFalse(TestSetArrayStrings.Contains("Starfruit"));
            Assert.IsFalse(TestSetArrayStrings.Contains("Watermelon"));

            Assert.IsTrue(TestSetEnumerableStrings.Contains("Eggplant"));
            Assert.IsTrue(TestSetEnumerableStrings.Contains("Fennel"));
            Assert.IsTrue(TestSetEnumerableStrings.Contains("Garlic"));
            Assert.IsFalse(TestSetEnumerableStrings.Contains("Artichoke"));
            Assert.IsFalse(TestSetEnumerableStrings.Contains("Brusselsprout"));
        }

        [TestMethod]
        public void TestEquality()
        {
            Assert.IsTrue(TestSetDefaultEmpty.SetEquals(TestSetDefaultEmpty));
            Assert.IsTrue(TestSetDefaultEmpty.SetEquals(TestSetArrayEmpty));
            Assert.IsTrue(TestSetArrayEmpty.SetEquals(TestSetEnumerableEmpty));
            Assert.IsTrue(TestSetArrayEmpty.SetEquals(TestSetDefaultEmpty));
            Assert.IsTrue(TestSetArrayEmpty.SetEquals(TestSetArrayEmpty));
            Assert.IsTrue(TestSetArrayEmpty.SetEquals(TestSetEnumerableEmpty));
            Assert.IsTrue(TestSetEnumerableEmpty.SetEquals(TestSetDefaultEmpty));
            Assert.IsTrue(TestSetEnumerableEmpty.SetEquals(TestSetArrayEmpty));
            Assert.IsTrue(TestSetEnumerableEmpty.SetEquals(TestSetEnumerableEmpty));

            Assert.IsTrue(TestSetArrayNumbers.SetEquals(TestSetArrayNumbers));
            Assert.IsTrue(TestSetArrayNumbers.SetEquals(TestSetEnumerableNumbers));
            Assert.IsTrue(TestSetEnumerableNumbers.SetEquals(TestSetArrayNumbers));
            Assert.IsTrue(TestSetEnumerableNumbers.SetEquals(TestSetEnumerableNumbers));

            Assert.IsTrue(TestSetArrayStrings.SetEquals(TestSetArrayStrings));
            Assert.IsFalse(TestSetArrayStrings.SetEquals(TestSetEnumerableStrings));
            Assert.IsFalse(TestSetEnumerableStrings.SetEquals(TestSetArrayStrings));
            Assert.IsTrue(TestSetEnumerableStrings.SetEquals(TestSetEnumerableStrings));

            Assert.IsFalse(TestSetArrayEmpty.SetEquals(TestSetArrayNumbers));
            Assert.IsFalse(TestSetEnumerableNumbers.SetEquals(TestSetEnumerableEmpty));
        }
    }
}
