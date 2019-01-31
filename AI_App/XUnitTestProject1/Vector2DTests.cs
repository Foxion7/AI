using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SteeringCS;
using Xunit;
using Xunit.Abstractions;
using Xunit.Extensions;
using Xunit.Sdk;
using Xunit.Serialization;

namespace XUnitTestProject1
{
    public class Vector2DTests
    {
        [Theory]
        //simply checking if adding produces the correct result

        //all positive
        [InlineData( 12,  4,  20,  50,  32,  54)]
        //one negative
        [InlineData(-12,  4,  20,  50,  8 ,  54)]
        [InlineData( 12, -4,  20,  50,  32,  46)]
        [InlineData( 12,  4, -20,  50, -8 ,  54)]
        [InlineData( 12,  4,  20, -50,  32, -46)]
        //two negative
        [InlineData(-12, -4,  20,  50,  8 ,  46)]
        [InlineData(-12,  4, -20,  50, -32,  54)]
        [InlineData(-12,  4,  20, -50,  8 , -46)]
        [InlineData( 12, -4, -20,  50, -8 ,  46)]
        [InlineData( 12, -4,  20, -50,  32, -54)]
        [InlineData( 12,  4, -20, -50, -8 , -46)]
        //three negative
        [InlineData(-12, -4, -20,  50, -32,  46)]
        [InlineData(-12, -4,  20, -50,  8 , -54)]
        [InlineData(-12,  4, -20, -50, -32, -46)]
        [InlineData( 12, -4, -20, -50, -8 , -54)]
        //four negative
        [InlineData(-12, -4, -20, -50, -32, -54)]

        public void AddTests(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1 + v2;

            //assert
            Assert.Equal(expected:vExpected, actual:vRes);
        }

        [Theory]
        //simply checking if subtracting produces the correct result
        //all positive
        [InlineData( 20,  50,  12,  4,  8 ,  46)]
        //one negative              
        [InlineData(-20,  50,  12,  4, -32,  46)]
        [InlineData( 20, -50,  12,  4,  8 , -54)]
        //two negative              
        [InlineData(-20, -50,  12,  4, -32, -54)]

        public void SubtractTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1 - v2;

            //assert
            Assert.Equal(expected:vExpected, actual:vRes);
        }

        [Theory]
        //(x1+x1,y1+y2) - (x1,y1) = (x2,y2)
        [InlineData(321, 987, 4523, 6345)]
        [InlineData(-76352, -736, 24, 1343)]
        [InlineData(544, 6542.2534, 52, 523)]
        [InlineData(6425.524, -625.42, 625, 523.5987)]
        [InlineData(2425.524, -624, 62452.645, -7562)]
        [InlineData(-625.5234, 987, 132, 32.1343)]
        public void SubtractFirstPartOfSumFromSumProducesSecondPartOfSum(int x1, int y1, int x2, int y2)
        {
            //arrange
            var sumX = x1 + x2;
            var sumY = y1 + y2;
            var v1 = new Vector2D(x1, y1);
            var v2 = new Vector2D(x2, y2);
            var sumV = new Vector2D(sumX, sumY);

            //act
            var res = sumV - v1;

            //assert
            Assert.Equal(expected: v2, actual: res);
        }
        [Theory]
        //(x1+x1,y1+y2) - (x2,y2) = (x1,y1)
        [InlineData(321, 987, 4523, 6345)]
        [InlineData(-76352, -736, 24, 1343)]
        [InlineData(544, 6542.2534, 52, 523)]
        [InlineData(6425.524, -625.42, 625, 523.5987)]
        [InlineData(2425.524, -624, 62452.645, -7562)]
        [InlineData(-625.5234, 987, 132, 32.1343)]
        public void SubtractSecondPartOfSumFromSumProducesFirstPartOfSum(int x1, int y1, int x2, int y2)
        {

            //arrange
            var sumX = x1 + x2;
            var sumY = y1 + y2;
            var v1 = new Vector2D(x1, y1);
            var v2 = new Vector2D(x2, y2);
            var sumV = new Vector2D(sumX, sumY);

            //act
            var res = sumV - v2;

            //assert
            Assert.Equal(expected: v1, actual: res);
        }
        [Theory]
        //(x1,y1) -(x1+x2,y1+y2) = (-x2, -y2)
        [InlineData(321, 987, 4523, 6345)]
        [InlineData(-76352, -736, 24, 1343)]
        [InlineData(544, 6542.2534, 52, 523)]
        [InlineData(6425.524, -625.42, 625, 523.5987)]
        [InlineData(2425.524, -624, 62452.645, -7562)]
        [InlineData(-625.5234, 987, 132, 32.1343)]
        public void SubtractSumFromFirstPartOfSumProducesSecondPartNegated(int x1, int y1, int x2, int y2)
        {
            //arrange
            var sumX = x1 + x2;
            var sumY = y1 + y2;
            var v1 = new Vector2D(x1, y1);
            var v2 = new Vector2D(x2, y2);
            var sumV = new Vector2D(sumX, sumY);

            //act
            var res = v1 - sumV;
            var expected = new Vector2D(-x2, -y2);

            //assert
            Assert.Equal(expected: expected, actual: res);
        }
        [Theory]
        //(x2,y2) -(x1+x2,y1+y2) = (-x1, -y1)
        [InlineData(321, 987, 4523, 6345)]
        [InlineData(-76352, -736, 24, 1343)]
        [InlineData(544, 6542.2534, 52, 523)]
        [InlineData(6425.524, -625.42, 625, 523.5987)]
        [InlineData(2425.524, -624, 62452.645, -7562)]
        [InlineData(-625.5234, 987, 132, 32.1343)]
        public void SubtractSumFromSecondPartOfSumProducesFirstPartNegated(int x1, int y1, int x2, int y2)
        {
            //arrange
            var sumX = x1 + x2;
            var sumY = y1 + y2;
            var v1 = new Vector2D(x1, y1);
            var v2 = new Vector2D(x2, y2);
            var sumV = new Vector2D(sumX, sumY);

            //act
            var res = v2 - sumV;
            var expected = new Vector2D(-x1, -y1);

            //assert
            Assert.Equal(expected: expected, actual: res);
        }

        [Theory]
        //simply checking if multiplication produces the correct result

        //no negative
        [InlineData(12, 4, 20, 240, 80)]
        //1 negative
        [InlineData(-12, 4, 20, -240, 80)]
        [InlineData(12, -4, 20, 240, -80)]
        [InlineData(12, 4, -20, -240, -80)]
        //2 negative
        [InlineData(-12, -4, 20, -240, -80)]
        [InlineData(-12, 4, -20, 240, -80)]
        [InlineData(12, -4, -20, -240, 80)]

        //3 negative
        [InlineData(-12, -4, -20, 240, 80)]

        public void MultTest(int x1, int y1, int op, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1 * op;

            //assert
            Assert.Equal(expected: vExpected, actual: vRes);
        }

        //simply checking if dividing a vector by a scalar produces the correct result
        [Theory]
        //no negative
        [InlineData(60, 48, 4, 15, 12)]
        //1 negative
        [InlineData(-60, 48, 4, -15, 12)]
        [InlineData(60, -48, 4, 15, -12)]
        [InlineData(60, 48, -4, -15, -12)]

        //2 negative
        [InlineData(-60, -48, 4, -15, -12)]
        [InlineData(60, -48, -4, -15, 12)]
        [InlineData(-60, 48, -4, 15, -12)]
        //3 negatives
        [InlineData(-60, -48, -4, 15, 12)]


        public void DivTest(int x1, int y1, int op, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1 / op;

            //assert
            Assert.Equal(expected: vExpected, actual: vRes);
        }

        [Theory]
        //simply checks whether the length is as expected
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 1.4142)]
        [InlineData(1, 2, 2.2361)]
        [InlineData(3, 4, 5)]
        [InlineData(6, 8, 10)]
        [InlineData(654, 453, 795.5658)]
        public void LengthTest(int x1, int y1, double expected)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);

            //act
            var actual = v1.Length();

            //assert
            Assert.Equal(expected:expected, actual:actual, precision:1);
        }

        [Theory]
        //simply checks whether the length squared is as expected
        [InlineData(0, 0, 0)]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 5)]
        [InlineData(3, 4, 25)]
        [InlineData(6, 8, 100)]
        [InlineData(654, 453, 632925)]

        public void SqLengthTest(int x1, int y1, double expected)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);

            //act
            var actual = v1.LengthSquared();

            //assert
            Assert.Equal(expected: expected, actual: actual, precision:1);
        }

        [Theory]
        //simply checks whether the normalization is as expected
        [InlineData(1, 1)]
        [InlineData(1, 2)]
        [InlineData(3, 4)]
        [InlineData(6, 8)]
        [InlineData(654, 453)]
        [InlineData(312, 2452.32)]
        [InlineData(6543.13, 234.24)]
        [InlineData(235666, 12.42)]
        [InlineData(0.9, 0.5)]

        public void NormalizationTest(int x1, int y1)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            var expected = 1;

            //act
            Vector2D normalized = v1.Normalize();
            var actual = normalized.Length();

            //assert
            Assert.Equal(expected: expected, actual: actual, precision: 1);
        }

        [Theory]
        //simply checks whether the normalization is as expected
        [InlineData(10, 10, 8)]
        [InlineData(100, 200, 54)]
        [InlineData(3000, 4, 2234)]
        [InlineData(60, 80, 65.4)]
        [InlineData(654, 453, 12)]
        [InlineData(312, 2452.32, 1342)]
        [InlineData(6543.13, 234.24, 999.342)]
        [InlineData(235666, 12.42, 13.2223)]
        [InlineData(0.9, 1.5, 1.4)]

        public void TruncateTest(int x1, int y1, double expectedAndMax)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);

            //act
            Vector2D normalized = v1.Truncate(expectedAndMax);
            var actual = normalized.Length();

            //assert
            Assert.Equal(expected: expectedAndMax, actual: actual, precision: 1);
        }
    }
}
