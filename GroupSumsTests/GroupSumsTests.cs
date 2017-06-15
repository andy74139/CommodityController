using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GroupSums.Tests
{
    [TestFixture]
    public class GroupSumsTests
    {
        private readonly IReadOnlyCollection<Product> _TestProducts = new ReadOnlyCollection<Product>
            (
            new List<Product>
            {
                new Product {Id = 1, Cost = 1, Revenue = 11, SellPrice = 21},
                new Product {Id = 2, Cost = 2, Revenue = 12, SellPrice = 22},
                new Product {Id = 3, Cost = 3, Revenue = 13, SellPrice = 23},
                new Product {Id = 4, Cost = 4, Revenue = 14, SellPrice = 24},
                new Product {Id = 5, Cost = 5, Revenue = 15, SellPrice = 25},
                new Product {Id = 6, Cost = 6, Revenue = 16, SellPrice = 26},
                new Product {Id = 7, Cost = 7, Revenue = 17, SellPrice = 27},
                new Product {Id = 8, Cost = 8, Revenue = 18, SellPrice = 28},
                new Product {Id = 9, Cost = 9, Revenue = 19, SellPrice = 29},
                new Product {Id = 10, Cost = 10, Revenue = 20, SellPrice = 30},
                new Product {Id = 11, Cost = 11, Revenue = 21, SellPrice = 31},
            }
            );

        [TestCase(3,      "Cost", ExpectedResult = new[] {  6, 15, 24, 21 }, TestName = "ThreeInGroup_GetCostSum_ShouldEqual")]
        [TestCase(3,   "Revenue", ExpectedResult = new[] { 36, 45, 54, 41 }, TestName = "ThreeInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(3, "SellPrice", ExpectedResult = new[] { 66, 75, 84, 61 }, TestName = "ThreeInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(4,      "Cost", ExpectedResult = new[] { 10,  26, 30 }, TestName = "FourInGroup_GetCostSum_ShouldEqual")]
        [TestCase(4,   "Revenue", ExpectedResult = new[] { 50,  66, 60 }, TestName = "FourInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(4, "SellPrice", ExpectedResult = new[] { 90, 106, 90 }, TestName = "FourInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(1,  "Cost", ExpectedResult = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, TestName = "OneInGroup_GetCostSum_ShouldListAllCosts")]
        [TestCase(11, "Cost", ExpectedResult = new[] { 66 }, TestName = "ElevenInGroup_GetCostSum_ShouldHaveOneSumOfCosts")]
        [TestCase(12, "Cost", ExpectedResult = new[] { 66 }, TestName = "TwelveInGroup_GetCostSum_ShouldHaveOneSumOfCosts")]
        public int[] GetGroupValueSumTest_ShouldEqual(int amountPerGroup, string dataName)
        {
            //Arrange
            var target = new GroupSums<Product>(_TestProducts);

            //Act
            var actual = target.Get(amountPerGroup, dataName);

            //Assert
            return actual.ToArray();
        }

        [TestCase(-1, "Cost", TestName = "NegativeNumberInGroup_GetCostSum_ShouldThrowArgumentException")]
        [TestCase(0, "Cost", TestName = "ZeroInGroup_GetCostSum_ShouldThrowArgumentException")]
        [TestCase(3, "WrongData", TestName = "PositiveNumberInGroup_GetWrongDataSum_ShouldThrowArgumentException")]
        [TestCase(3, null, TestName = "PositiveNumberInGroup_GetNullSum_ShouldThrowArgumentException")]
        [TestCase(3, "", TestName = "PositiveNumberInGroup_GetEmptySum_ShouldThrowArgumentException")]
        [TestCase(0, "WrongData", TestName = "ZeroInGroup_GetWrongDataSum_ShouldThrowArgumentException")]
        public void GetGroupValueSumTest_ShouldThrowException(int amountPerGroup, string dataName)
        {
            //Arrange
            var target = new GroupSums<Product>(_TestProducts);

            //Act & Assert
            TestDelegate action = () => target.Get(amountPerGroup, dataName);
            Assert.Throws<ArgumentException>(action);
        }
    }
}
