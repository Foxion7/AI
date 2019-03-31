using System;
using SteeringCS.Fuzzylogic.FuzzySets;
using Xunit;

namespace XUnitTestProject1.FuzzyLogicTests
{
    public class FuzzySetRightShoulderTest
    {

        [Theory]
        [InlineData(20)]
        [InlineData(.53)]
        [InlineData(-53)]
        [InlineData(123)]
        public void ifInputValueIsEqualToPeakOfRightShoulder_ThenFuzzyfiyingItReturnsOne(int center)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetTriangle(center, 20, 10);
            //act
            var actual = set.CalculateDOM(center);
            var expected = 1;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(.53)]
        [InlineData(-53)]
        [InlineData(123)]
        public void ifInputValueIsHigherThanThePeakOfRightShoulder_ThenFuzzyfiyingItReturnsOne(int center)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetRightShoulder(center, 20, 10);
            //act
            var actual = set.CalculateDOM(center+100);
            var expected = 1;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }

        [Theory]
        [InlineData(20, 10)]
        [InlineData(.53, 14.53)]
        [InlineData(-53, 32)]
        [InlineData(123, 0)]
        public void ifInputValueIsLowerThanTheLeftEndOfARightShoulder_ThenFuzzyfiyingItReturnsZero(int center, int left)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetRightShoulder(center, left, 10);
            //act
            var actual = set.CalculateDOM(center - left - 1);
            var expected = 0;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }

        [Theory]
        [InlineData(20, 10)]
        [InlineData(.53, 14.53)]
        [InlineData(-53, 32)]
        [InlineData(123, 312)]
        public void ifInputValueIsExactlyBetweenPeakAndLeftEndOfRightShoulder_ThenFuzzyfiyingItReturnsHalf(int center, int left)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetRightShoulder(center, left,20);
            //act
            var actual = set.CalculateDOM(center - (0.5 * left));
            var expected = 0.5;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }

        [Theory]
        [InlineData(20, 10, 0.2)]
        [InlineData(.53, 14.53, 0.5)]
        [InlineData(-53, 32, 1.5)]
        [InlineData(123, 312, 0.6)]
        [InlineData(64, 32, -0.3)]
        [InlineData(23, 7, -0.6)]
        [InlineData(3, 12, 532)]
        //honestly I couldn't give this test a good name. What it tests is this: If a value is x% between the center and the left end than the 
        //Calculated Dom should be exactly that 1 - (x% of 1)
        public void ifInputValueIsSomePercentageBetweennPeakAndLeftEndOfRightShoulder_ThenFuzzyfiyingItReturnsOneMinusThatPerc
            (int center, int left, double perc)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetRightShoulder(center, left, 20);
            //act
            var actual = set.CalculateDOM(center - (perc * left));
            var expected = 1 - perc;
            if (expected > 1) expected = 1;
            if (expected < 0) expected = 0;
            //assert
            Assert.True(Math.Abs(expected-actual) < 0.001);
        }
    }
}