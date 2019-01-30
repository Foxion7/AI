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
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void AddTests(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1,y1);
            Vector2D v2 = new Vector2D(x2,y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);
            
            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void subtractTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void multTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void divTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }
        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void lengthTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 0)]
        [InlineData(4, 5, 0, 0, 4, 5)]
        [InlineData(0, 0, 12, 55, 12, 55)]
        [InlineData(12, 4, 20, 50, 32, 54)]
        [InlineData(-4, -5, 0, 0, -4, -5)]
        [InlineData(0, 0, -12, -55, -12, -55)]
        [InlineData(-12, -4, -20, -50, -32, -54)]
        [InlineData(12, 4, -20, -50, -8, -46)]
        [InlineData(-12, -4, 20, 50, 8, 46)]
        public void sqLengthTest(int x1, int y1, int x2, int y2, int xExp, int yExp)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D vExpected = new Vector2D(xExp, yExp);

            //act
            Vector2D vRes = v1.Add(v2);

            //assert
            Assert.Equal(vRes, vExpected);
        }
    }
}
