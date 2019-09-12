using System;
using System.Text;
using System.Collections.Generic;
using FlexLib.Sets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests.Sets
{
    [TestClass]
    public class FiniteSequenceTest
    {
        private FiniteSequence<int> TestSequenceDefaultEmpty;
        private FiniteSequence<int> TestSequenceArrayEmpty;
        private FiniteSequence<int> TestSequenceEnumerableEmpty;
        private FiniteSequence<int> TestSequenceArrayNumbers;
        private FiniteSequence<int> TestSequenceEnumerableNumbers;
        private FiniteSequence<string> TestSequenceArrayStrings;
        private FiniteSequence<string> TestSequenceEnumerableStrings;

        [TestInitialize]
        public void TestInitialize()
        {
            TestSequenceDefaultEmpty = new FiniteSequence<int>();
            TestSequenceArrayEmpty = new FiniteSequence<int>(new int[] { });
            TestSequenceEnumerableEmpty = new FiniteSequence<int>(new List<int> { });

            TestSequenceArrayNumbers = new FiniteSequence<int>(new int[] { 2, 1, 6 });
            TestSequenceEnumerableNumbers = new FiniteSequence<int>(new List<int> { 2, 1, 6 });

            TestSequenceArrayStrings = new FiniteSequence<string>(new string[] { "Apple", "Banana", "Cherry" });
            TestSequenceEnumerableStrings = new FiniteSequence<string>(new List<string> { "Cherry", "Apple", "Banana" });
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
