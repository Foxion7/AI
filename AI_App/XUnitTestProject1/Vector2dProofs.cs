using System;
using System.Collections.Generic;
using System.Text;
using SteeringCS;
using Xunit;

namespace XUnitTestProject1
{
    public class Vector2DProofs
    {
        [Theory]
        //(0,0) is the ID of + on vectors
        [InlineData(0, 0)]
        [InlineData(4, 5)]
        [InlineData(523, 5)]
        [InlineData(-53421, 5)]
        [InlineData(4, -523.131)]
        [InlineData(-11114, 5)]
        [InlineData(4, 6666)]
        [InlineData(-134, 5)]
        [InlineData(4, .012)]
        public void AddIdTest(int x, int y)
        {
            //arrange
            Vector2D ID = new Vector2D(0,0);
            Vector2D v = new Vector2D(x, y);

            //act
            Vector2D vRes = v + ID;

            //assert
            Assert.Equal(expected: v, actual: vRes);
        }

        [Theory]
        // commutativity of + on vectors
        [InlineData(0, 0, 5 ,3)]
        [InlineData(4, 5 , 134, 3)]
        [InlineData(523, 5, 0.34, 776)]
        [InlineData(-53421, 5, -543, -65)]
        [InlineData(111, 222, 333, 444)]
        [InlineData(-11114, 5, 9.243,134 )]
        [InlineData(4, 6666 , 555, 321)]
        [InlineData(-134, 5, 013.53421,678976)]
        [InlineData(4, .012 , 987, -76543)]
        public void AddCommutativityTest(int x1, int y1, int x2, int y2)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);

            //act
            var res1 = v1 + v2;
            var res2 = v2 + v1;
            //assert
            Assert.Equal(v1+v2, v2+v1);
        }

        [Theory]
        // associativity of + on vectors
        [InlineData(0, 0, 5, 3,95,-43)]
        [InlineData(4, 5, 134, 3,3.54,1.434)]
        [InlineData(523, 5, 0.34, 776, 9.31,222)]
        [InlineData(-53421, 5, -543, -65,0.2,0.5)]
        [InlineData(111,222,333,444,555,666)]
        [InlineData(-11114, 5, 9.243, 134,-5423.6542,542)]
        [InlineData(4, 6666, 555, 321, 9,5234)]
        [InlineData(-134, 5, 013.53421, 678976, -5234.2534,232)]
        [InlineData(4, .012, 987, -76543, 85554,65431)]
        public void AddAssociativityTest(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            //arrange
            Vector2D v1 = new Vector2D(x1, y1);
            Vector2D v2 = new Vector2D(x2, y2);
            Vector2D v3 = new Vector2D(x3, y3);

            //act
            var res1 = (v1 + v2) + v3;
            var res2 = v2 + (v1 + v3);
            //assert
            Assert.Equal(v1 + v2, v2 + v1);
        }

        [Theory]
        //(0,0) is the id of - on vector
        [InlineData(321, 987)]
        [InlineData(-76352, -736)]
        [InlineData(544, 6542.2534)]
        [InlineData(6425.524, -625.42)]
        [InlineData(2425.524, -624)]
        [InlineData(-625.5234, 987)]
        public void MinusIdProof(int x, int y)
        {
            //arrange
            var v = new Vector2D(x, y);
            var ID = new Vector2D(0, 0);

            //act
            var actual = v - ID;
            //assert
            Assert.Equal(expected: v, actual: actual);
        }
        [Theory]
        //vector minus itself is (0,0)
        [InlineData(321, 987)]
        [InlineData(-76352, -736)]
        [InlineData(544, 6542.2534)]
        [InlineData(6425.524, -625.42)]
        [InlineData(2425.524, -624)]
        [InlineData(-625.5234, 987)]
        public void MinusNullification(int x, int y)
        {
            //arrange
            var v = new Vector2D(x, y);
            var expected = new Vector2D(0,0);

            //act
            var actual = v - v;
            //assert
            Assert.Equal(expected: expected, actual: actual);
        }
        [Theory]
        //(0,0) - (a,b) = (-a,-b)
        [InlineData(321, 987)]
        [InlineData(-76352, -736)]
        [InlineData(544, 6542.2534)]
        [InlineData(6425.524, -625.42)]
        [InlineData(2425.524, -624)]
        [InlineData(-625.5234, 987)]
        public void NegationOfAVector(int x, int y)
        {
            //arrange
            var v = new Vector2D(x, y);
            var zerozero = new Vector2D(0,0);
            var expected = new Vector2D(-x, -y);

            //act
            var actual = zerozero - v;
            //assert
            Assert.Equal(expected: expected, actual: actual);
        }

        [Theory]
        //1 is the ID of * on vectors
        [InlineData(0, 0)]
        [InlineData(4, 5)]
        [InlineData(523, 5)]
        [InlineData(-53421, 5)]
        [InlineData(4, -523.131)]
        [InlineData(-11114, 5)]
        [InlineData(4, 6666)]
        [InlineData(-134, 5)]
        [InlineData(4, .012)]
        public void ProductIdTest(int x, int y)
        {
            //arrange
            Vector2D v = new Vector2D(x, y);

            //act
            Vector2D vRes = v * 1;

            //assert
            Assert.Equal(expected: v, actual: vRes);
        }

        [Theory]
        //0 is the cons of * on vectors
        [InlineData(0, 0)]
        [InlineData(4, 5)]
        [InlineData(523, 5)]
        [InlineData(-53421, 5)]
        [InlineData(4, -523.131)]
        [InlineData(-11114, 5)]
        [InlineData(4, 6666)]
        [InlineData(-134, 5)]
        [InlineData(4, .012)]
        public void ProductCons1Test(int x, int y)
        {
            //arrange
            Vector2D v = new Vector2D(x, y);
            Vector2D cons = new Vector2D(0,0);

            //act
            Vector2D vRes = v * 0;

            //assert
            Assert.Equal(expected: cons, actual: vRes);
        }

        [Theory]
        //(0,0) is the const of * on vectors
        [InlineData(0)]
        [InlineData(4)]
        [InlineData(523.23)]
        [InlineData(-53421)]
        [InlineData(4.42)]
        [InlineData(-11114)]
        [InlineData(-134)]
        public void ProductCons2Test(int with)
        {
            //arrange
            Vector2D cons = new Vector2D(0, 0);

            //act
            Vector2D vRes = cons * with;

            //assert
            Assert.Equal(expected: cons, actual: vRes);
        }


        [Theory]
        //1 is the ID of / on vectors
        [InlineData(0, 0)]
        [InlineData(4, 5)]
        [InlineData(523, 5)]
        [InlineData(-53421, 5)]
        [InlineData(4, -523.131)]
        [InlineData(-11114, 5)]
        [InlineData(4, 6666)]
        [InlineData(-134, 5)]
        [InlineData(4, .012)]
        public void DivisionIdTest(int x, int y)
        {
            //arrange
            Vector2D v = new Vector2D(x, y);

            //act
            Vector2D vRes = v / 1;

            //assert
            Assert.Equal(expected: v, actual: vRes);
        }

        [Theory]
        // (0,0) is the cons of / on vectors
        [InlineData(4)]
        [InlineData(523.23)]
        [InlineData(-53421)]
        [InlineData(4.42)]
        [InlineData(-11114)]
        [InlineData(-134)]
        public void DivisionConsTest(int with)
        {
            //arrange
            Vector2D cons = new Vector2D(0, 0);

            //act
            Vector2D vRes = cons / with;

            //assert
            Assert.Equal(expected: cons, actual: vRes);
        }

    }
}
