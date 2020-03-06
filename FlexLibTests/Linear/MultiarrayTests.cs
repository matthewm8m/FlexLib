using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Linear;

namespace FlexLibTests.Linear
{
    [TestFixture]
    public class MultiarrayTests
    {
        private Dictionary<string, Multiarray<int>> TestIntegerArrays;

        [SetUp]
        protected void SetUp()
        {
            TestIntegerArrays =
            new Dictionary<string, Multiarray<int>>()
            {
                {"A", Multiarray<int>.FromArray(new int[] {
                    1, 3, 5, 2, 4, 6, 8
                })},
                {"B", Multiarray<int>.FromArray(new int[,] {
                    {1, 2, 3},
                    {4, 5, 6}
                })},
                {"C", Multiarray<int>.FromArray(new int[,,] {
                    {{-4,-2,-1}, {1, 4, 2}, { 5, 3, 1}, {-6, 3,  13}},
                    {{ 0, 0, 1}, {1, 2, 3}, {-4, 0, 5}, { 7, 6, -1}}
                })},
                {"D", Multiarray<int>.FromArray(new int[,,,] {
                    {{{1, 2, 3}, {4, 5, 6}}, {{7, 8, 9}, {0, 1, 2}}},
                    {{{3, 4, 5}, {6, 7, 8}}, {{9, 0, 1}, {2, 3, 4}}},
                    {{{5, 6, 7}, {8, 9, 0}}, {{1, 2, 3}, {4, 5, 6}}}
                })},
                {"E", Multiarray<int>.FromArray(new int[,] {
                    {1, 3, 5, 2, 4, 6, 8}
                })},
                {"F", Multiarray<int>.FromArray(new int[,,] {
                    {{-4,-2,-1}, {1, 4, 2}, { 5, 3, 1}, {-6, 3,  13}},
                    {{ 0, 0, 1}, {1, 2, 3}, {-4, 0, 5}, { 7, 6, -1}}
                })},
                {"G", new Multiarray<int>(new int[] {
                    5, 2, 8
                }, new int[] {3})},
                {"H", new Multiarray<int>(new int[] {
                    1, 3, 4, 6
                }, new int[] {2, 2})},
                {"I", new Multiarray<int>(new int[] {
                    0, 0, 1, 2, -4, 0,  7, 6
                }, new int[] {1, 4, 2})},
                {"J", Multiarray<int>.FromArray(new int[] {
                    1, 3, 5, 3, 4, 6, 8
                })},
                {"K", Multiarray<int>.FromArray(new int[] {})},
            };

            TestIntegerArrays.Add("A'", TestIntegerArrays["A"].Subarray(axis: 0, 2, 3, 6));
            TestIntegerArrays.Add("B'", TestIntegerArrays["B"].Subarray(axis: 1, new int[] { 0, 2 })
                                                              .Subarray(axis: 0, new int[] { 0, 1 }));
            TestIntegerArrays.Add("C'", TestIntegerArrays["C"].Subarray(new int[][] {
                    new int[] {1},
                    null,
                    new int[] {0, 1}
                }));

            TestIntegerArrays.Add("B''", TestIntegerArrays["B'"].Subarray(axis: 0, new int[] { 1, 0, 1 }));
        }

        [TestCase("A", 7)]
        [TestCase("B", 6)]
        [TestCase("C", 24)]
        [TestCase("D", 36)]
        public void TestSize(string arrayKey, int size)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(size, array.Size);
        }

        [TestCase("A", new int[] { 7 })]
        [TestCase("B", new int[] { 2, 3 })]
        [TestCase("C", new int[] { 2, 4, 3 })]
        [TestCase("D", new int[] { 3, 2, 2, 3 })]
        public void TestOrder(string arrayKey, int[] order)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(order, array.Order);
        }

        [TestCase("A", 1)]
        [TestCase("B", 2)]
        [TestCase("C", 3)]
        [TestCase("D", 4)]
        public void TestDegree(string arrayKey, int degree)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(degree, array.Degree);
        }

        [TestCase("A", new int[] { 4 }, 4)]
        [TestCase("B", new int[] { 1, 1 }, 5)]
        [TestCase("C", new int[] { 0, 2, 2 }, 1)]
        [TestCase("D", new int[] { 1, 0, 0, 2 }, 5)]
        [TestCase("A'", new int[] { 1 }, 2)]
        [TestCase("B'", new int[] { 1, 0 }, 4)]
        [TestCase("B'", new int[] { 0, 1 }, 3)]
        public void TestElements(string arrayKey, int[] indices, int element)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(element, array[indices]);
        }

        [TestCase("A", 4, 4)]
        [TestCase("B", 4, 5)]
        [TestCase("C", 8, 1)]
        [TestCase("D", 14, 5)]
        [TestCase("A'", 1, 2)]
        [TestCase("B'", 2, 4)]
        public void TestElements(string arrayKey, int index, int element)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(element, array[index]);
        }

        [TestCase("A", new int[] { 1, 3, 5, 2, 4, 6, 8 })]
        [TestCase("B", new int[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase("A'", new int[] { 5, 2, 8 })]
        [TestCase("B'", new int[] { 1, 3, 4, 6 })]
        [TestCase("B''", new int[] { 4, 6, 1, 3, 4, 6 })]
        public void TestArrayEnumerate(string arrayKey, int[] elements)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];

            int k = 0;
            foreach (int element in array)
                Assert.AreEqual(elements[k++], element);

            k = 0;
            int count = 0;
            foreach (object obj in (IEnumerable)array)
            {
                count++;
                Assert.AreEqual(elements[k++], (int)obj);
            }
            Assert.AreEqual(count, elements.Length);
        }

        [TestCase("A", new int[] { -1 })]
        [TestCase("B", new int[] { 0, 3 })]
        [TestCase("C", new int[] { 1, 6, 6 })]
        [TestCase("D", new int[] { 2, 1, 0, -1 })]
        public void TestIndexOutOfRange(string arrayKey, int[] indices)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                int value = array[indices];
            });
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                array[indices] = 0;
            });
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                array.RavelIndex(indices);
            });
        }

        [TestCase("A", -1)]
        [TestCase("B", 6)]
        [TestCase("C", 26)]
        [TestCase("D", -100)]
        public void TestIndexOutOfRange(string arrayKey, int index)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                int value = array[index];
            });
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                array[index] = 0;
            });
            Assert.Throws<IndexOutOfRangeException>(delegate
            {
                array.UnravelIndex(index);
            });
        }

        [TestCase("A", new int[] { 3, 0 })]
        [TestCase("B", new int[] { 3 })]
        [TestCase("C", new int[] { 1, 6, 6, 2, 1, 0 })]
        [TestCase("D", new int[] { })]
        public void TestRankMismatch(string arrayKey, int[] indices)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.Throws<RankException>(delegate
            {
                int value = array[indices];
            });
            Assert.Throws<RankException>(delegate
            {
                array[indices] = 0;
            });
            Assert.Throws<RankException>(delegate
            {
                array.RavelIndex(indices);
            });
        }

        [TestCase(new int[] { 1, 3, 1 }, new int[] { 0, 2, 0 })]
        [TestCase(new int[] { 5, 3 }, new int[] { 2, 0 })]
        [TestCase(new int[] { 2, 1, 2 }, new int[] { 1, 0, 0 })]
        public void TestRavelUnravel(int[] shape, int[] indices)
        {
            Multiarray<int> array = new Multiarray<int>(shape);
            Assert.AreEqual(indices, array.UnravelIndex(array.RavelIndex(indices)));
            Assert.AreEqual(array[indices], array[array.RavelIndex(indices)]);
        }

        [TestCase(new int[] { 1, 3, 1 }, 2)]
        [TestCase(new int[] { 5, 3 }, 10)]
        [TestCase(new int[] { 2, 1, 2 }, 2)]
        public void TestUnravelRavel(int[] shape, int index)
        {
            Multiarray<int> array = new Multiarray<int>(shape);
            Assert.AreEqual(index, array.RavelIndex(array.UnravelIndex(index)));
            Assert.AreEqual(array[index], array[array.UnravelIndex(index)]);
        }

        [TestCase("A", "A", true)]
        [TestCase("D", "D", true)]
        [TestCase("B", "C", false)]
        [TestCase("A", "E", false)]
        [TestCase("C", "F", true)]
        [TestCase("G", "A'", true)]
        [TestCase("G", "B'", false)]
        [TestCase("H", "B'", true)]
        [TestCase("I", "B'", false)]
        [TestCase("I", "C'", true)]
        [TestCase("A", "J", false)]
        [TestCase("B", "B'", false)]
        public void TestEquality(string arrayKeyA, string arrayKeyB, bool equal)
        {
            Multiarray<int> arrayA = TestIntegerArrays[arrayKeyA];
            Multiarray<int> arrayB = TestIntegerArrays[arrayKeyB];

            Assert.AreEqual(equal, arrayA == arrayB);
            Assert.AreEqual(!equal, arrayA != arrayB);
            Assert.AreEqual(!equal, !(arrayA == arrayB));
            Assert.AreEqual(equal, !(arrayA != arrayB));
            Assert.AreEqual(equal, arrayA.Equals(arrayB));
            Assert.AreEqual(equal, arrayB.Equals(arrayA));
            Assert.AreEqual(!equal, !arrayA.Equals(arrayB));
            Assert.AreEqual(!equal, !arrayB.Equals(arrayA));
        }

        [TestCase("A")]
        [TestCase("C")]
        [TestCase("K")]
        public void TestNullEquality(string arrayKey)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];

            Assert.AreEqual(true, null == null);
            Assert.AreEqual(false, null != null);
            Assert.AreEqual(false, null == array);
            Assert.AreEqual(true, null != array);
            Assert.AreEqual(false, array == null);
            Assert.AreEqual(true, array != null);
        }

        [TestCase("A", new int[] { 4 })]
        [TestCase("B", new int[] { 1, 1 })]
        [TestCase("C", new int[] { 0, 2, 2 })]
        [TestCase("D", new int[] { 1, 0, 0, 2 })]
        public void TestModification(string arrayKey, int[] indices)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];

            int value = array[indices];

            array[indices] += 1;
            Assert.AreEqual(value + 1, array[indices]);

            array[indices] -= 1;
            Assert.AreEqual(value, array[indices]);
        }

        [TestCase("A", 4)]
        [TestCase("B", 3)]
        [TestCase("C", 9)]
        [TestCase("D", 12)]
        public void TestModification(string arrayKey, int index)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];

            int value = array[index];

            array[index] += 1;
            Assert.AreEqual(value + 1, array[index]);

            array[index] -= 1;
            Assert.AreEqual(value, array[index]);
        }

        [TestCase("A", "A'", new int[] { 2 }, new int[] { 0 })]
        [TestCase("A", "A'", new int[] { 3 }, new int[] { 1 })]
        [TestCase("B", "B'", new int[] { 0, 2 }, new int[] { 0, 1 })]
        [TestCase("B", "B'", new int[] { 1, 0 }, new int[] { 1, 0 })]
        public void TestSubarrayModification(string arrayKey, string subarrayKey, int[] arrayIndices, int[] subarrayIndices)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Multiarray<int> subarray = TestIntegerArrays[subarrayKey];

            int value = array[arrayIndices];

            array[arrayIndices] += 1;
            Assert.AreEqual(value + 1, array[arrayIndices]);
            Assert.AreEqual(value + 1, subarray[subarrayIndices]);

            subarray[subarrayIndices] -= 1;
            Assert.AreEqual(value, array[arrayIndices]);
            Assert.AreEqual(value, subarray[subarrayIndices]);
        }

        [TestCase("A", new int[] { 1, 7, 1 }, new int[] { 0, 3, 0 }, 2)]
        [TestCase("B", new int[] { 3, 2 }, new int[] { 2, 0 }, 5)]
        [TestCase("J", new int[] { 1, 7, 1 }, new int[] { 0, 3, 0 }, 3)]
        public void TestReorder(string arrayKey, int[] order, int[] indices, int element)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Multiarray<int> arrayReorder = array.Reordered(order);

            Assert.AreEqual(array.Size, arrayReorder.Size);
            Assert.AreEqual(order, arrayReorder.Order);
            Assert.AreEqual(element, arrayReorder[indices]);
        }

        [TestCase(0.0, new int[] { 3, 3 }, new int[] { 0, 2 })]
        [TestCase(-3.1, new int[] { 5 }, new int[] { 3 })]
        [TestCase(double.Epsilon, new int[] { 2, 2, 3 }, new int[] { 1, 1, 1 })]
        [TestCase(double.PositiveInfinity, new int[] { 5, 4, 3, 2, 1 }, new int[] { 4, 2, 2, 0, 0 })]
        public void TestFilled(double fillValue, int[] arrayShape, int[] arrayIndex)
        {
            Multiarray<double> array = Multiarray<double>.Filled(arrayShape, fillValue);
            Assert.AreEqual(fillValue, array[arrayIndex]);
        }

        [TestCase(new object[] { 1.0d, 2.0d, 3.0d }, false)]
        [TestCase(new object[] { (int)1, 2.0d, (float)3.0 }, true)]
        [TestCase(new object[] { "a", 2, "thing" }, true)]
        public void TestArrayTypeMismatch(object[] data, bool fails)
        {
            if (fails)
            {
                Assert.Throws<ArrayTypeMismatchException>(delegate
                {
                    Multiarray<double> array = Multiarray<double>.FromArray(data);
                });
            }
            else
            {
                Assert.DoesNotThrow(delegate
                {
                    Multiarray<double> array = Multiarray<double>.FromArray(data);
                });
            }
        }

        [TestCase("A", true)]
        [TestCase("B", false)]
        [TestCase("A'", true)]
        [TestCase("B'", false)]
        [TestCase("E", false)]
        public void TestIsVector(string arrayKey, bool isVector)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(isVector, array.IsVector());
        }

        [TestCase("A", false)]
        [TestCase("B", true)]
        [TestCase("A'", false)]
        [TestCase("B'", true)]
        [TestCase("H", true)]
        public void TestIsMatrix(string arrayKey, bool isMatrix)
        {
            Multiarray<int> array = TestIntegerArrays[arrayKey];
            Assert.AreEqual(isMatrix, array.IsMatrix());
        }
    }
}