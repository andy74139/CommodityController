using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace GroupSums.Tests
{
    [TestFixture]
    public class GroupSumsTests
    {
        private readonly List<Product> _TestProducts = new List<Product>()
        {
            new Product(1, 1, 11, 21),
            new Product(2, 2, 12, 22),
            new Product(3, 3, 13, 23),
            new Product(4, 4, 14, 24),
            new Product(5, 5, 15, 25),
            new Product(6, 6, 16, 26),
            new Product(7, 7, 17, 27),
            new Product(8, 8, 18, 28),
            new Product(9, 9, 19, 29),
            new Product(10, 10, 20, 30),
            new Product(11, 11, 21, 31), 
        };

        [TestCase(3,      "Cost", ExpectedResult = new[] {  6, 15, 24, 21 }, TestName = "ThreeInGroup_GetCostSum_ShouldEqual")]
        [TestCase(3,   "Revenue", ExpectedResult = new[] { 36, 45, 54, 41 }, TestName = "ThreeInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(3, "SellPrice", ExpectedResult = new[] { 66, 75, 84, 61 }, TestName = "ThreeInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(4,      "Cost", ExpectedResult = new[] { 10,  26, 30 }, TestName = "FourInGroup_GetCostSum_ShouldEqual")]
        [TestCase(4,   "Revenue", ExpectedResult = new[] { 50,  66, 60 }, TestName = "FourInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(4, "SellPrice", ExpectedResult = new[] { 90, 106, 90 }, TestName = "FourInGroup_GetRevenueSum_ShouldEqual")]
        [TestCase(1,  "Cost", ExpectedResult = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 }, TestName = "OneInGroup_GetCostSum_ShouldListAllCosts")]
        [TestCase(11, "Cost", ExpectedResult = new[] { 66 }, TestName = "ElevenInGroup_GetCostSum_ShouldHaveOneSumOfCosts")]
        [TestCase(12, "Cost", ExpectedResult = new[] { 66 }, TestName = "TwelveInGroup_GetCostSum_ShouldHaveOneSumOfCosts")]
        public int[] GetGroupValueSumTest_ShouldEqual(int amountPerGroup, string columnName)
        {
            //Arrange
            var target = new GroupSums(_TestProducts);

            //Act
            var actual = target.Get(amountPerGroup, columnName);

            //Assert
            return actual.ToArray();
        }

        [TestCase(-1, "Cost", TestName = "NegativeNumberInGroup_GetCostSum_ShouldThrowArgumentException")]
        [TestCase(0, "Cost", TestName = "ZeroInGroup_GetCostSum_ShouldThrowArgumentException")]
        [TestCase(3, "WrongData", TestName = "PositiveNumberInGroup_GetWrongDataSum_ShouldThrowArgumentException")]
        [TestCase(3, null, TestName = "PositiveNumberInGroup_GetNullSum_ShouldThrowArgumentException")]
        [TestCase(3, "", TestName = "PositiveNumberInGroup_GetEmptySum_ShouldThrowArgumentException")]
        [TestCase(0, "WrongData", TestName = "ZeroInGroup_GetWrongDataSum_ShouldThrowArgumentException")]
        public void GetGroupValueSumTest_ShouldThrowException(int amountPerGroup, string columnName)
        {
            //Arrange
            var target = new GroupSums(_TestProducts);

            //Act & Assert
            TestDelegate action = () => target.Get(amountPerGroup, columnName);
            Assert.Throws<ArgumentException>(action);
        }
    }
}
