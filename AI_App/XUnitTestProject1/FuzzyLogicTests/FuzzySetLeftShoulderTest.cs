using System;
using SteeringCS.Fuzzylogic.FuzzySets;
using Xunit;

namespace XUnitTestProject1
{
    public class FuzzySetLeftShoulderTest
    {

        [Theory]
        [InlineData(20)]
        [InlineData(.53)]
        [InlineData(-53)]
        [InlineData(123)]
        public void ifInputValueIsEqualToPeakOfLeftShoulder_ThenFuzzyfiyingItReturnsOne(int center)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetLeftShoulder(center, 20, 23);
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
        public void ifInputValueIsLowerThanThePeakEndOfALeftShoulder_ThenFuzzyfiyingItReturnsOne(int center)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetLeftShoulder(center, 20, 43);
            //act
            var actual = set.CalculateDOM(center - 100);
            var expected = 1;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }


        [Theory]
        [InlineData(20, 10)]
        [InlineData(.53, 14.53)]
        [InlineData(-53, 32)]
        [InlineData(123, 0)]
        public void ifInputValueIsHigherThanTheRightEndOfALeftShoulder_ThenFuzzyfiyingItReturnsZero(int center,
            int right)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetLeftShoulder(center, 20, right);
            //act
            var actual = set.CalculateDOM(center + right + 1);
            var expected = 0;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }

        [Theory]
        [InlineData(20, 10)]
        [InlineData(.53, 14.53)]
        [InlineData(-53, 32)]
        [InlineData(123, 312)]
        public void ifInputValueIsExactlyBetweenPeakAndRightEndOfLeftShoulder_ThenFuzzyfiyingItReturnsHalf(int center, int right)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetLeftShoulder(center, 20, right);
            //act
            var actual = set.CalculateDOM(center + (0.5*right));
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
        //honestly I couldn't give this test a good name. What it tests is this: If a value is x% between the center and the right end than the 
        //Calculated Dom should be exactly that 1 - (x% of 1)
        public void ifInputValueIsSomePercentageBetweennPeakAndRightEndOfLeftShoulder_ThenFuzzyfiyingItReturnsOneMinusThatPerc
            (int center, int right, double perc)
        {
            //arrange
            //this triangle is 10,20,30
            var set = new FuzzySetLeftShoulder(center, 20, right);
            //act
            var actual = set.CalculateDOM(center + (perc * right));
            var expected = 1 - perc;
            if (expected > 1) expected = 1;
            if (expected < 0) expected = 0;
            //assert
            Assert.True(Math.Abs(expected - actual) < 0.001);
        }
    }
}