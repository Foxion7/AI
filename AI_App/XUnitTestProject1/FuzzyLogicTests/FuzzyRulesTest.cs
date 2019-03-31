using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteeringCS.Fuzzylogic;
using SteeringCS.Fuzzylogic.FuzzyTerms;
using Xunit;

namespace XUnitTestProject1.FuzzyLogicTests
{
    public class FuzzyRulesTest
    {
        public FuzzyModule Mod;
        public FuzzyVariable AVar;
        public FuzzyVariable BVar;
        public FuzzyVariable CVar;
        public FuzzyRulesTest()
        {
            Mod = new FuzzyModule();
            AVar = Mod.CreateFLV("AV");
            BVar = Mod.CreateFLV("BV");
            CVar = Mod.CreateFLV("CV");
        }
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-4)]
        [InlineData(0)]
        [InlineData(753)]
        [InlineData(13)]
        [InlineData(16)]
        public void WithRuleAToB_TheConfidenceInAWillBeTheSameAsInB(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = BVar.AddRightShoulderSet("BS", 5, 16, 50);
            var r = Mod.addRule(ASet, BSet);

            //act
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");
            var expected = ASet.GetDom();
            var actual = BSet.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(1, 5)]
        [InlineData(6, 3)]
        [InlineData(-4, 7)]
        [InlineData(0, 9)]
        [InlineData(753, 345)]
        [InlineData(13, 23)]
        [InlineData(16, 10)]
        public void WithRuleAToB_TheConfidenceInAWillBeTheSameAsInB_UsingAndForA(int t1, int t2)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = BVar.AddRightShoulderSet("BS", 5, 16, 50);
            var CSet = CVar.AddLeftShoulderSet("CS", 0, 100, 30);
            var AAndB = new FzAnd(ASet, BSet);
            Mod.addRule(AAndB, BSet);

            //act
            Mod.Fuzzify("AV", t1);
            Mod.Fuzzify("BV", t2);
            Mod.DeFuzzify("CV");
            var expected = AAndB.GetDom();
            var actual = CSet.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-4)]
        [InlineData(0)]
        [InlineData(753)]
        [InlineData(13)]
        [InlineData(16)]
        public void AndTakesMinBetweenTerms(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = AVar.AddTriangularSet("BS", 5, 16, 50);
            var AandB = new FzAnd(ASet, BSet);


            //act
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");
            var expected = Math.Min(ASet.GetDom(), BSet.GetDom());
            var actual = AandB.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-4)]
        [InlineData(0)]
        [InlineData(753)]
        [InlineData(13)]
        [InlineData(16)]
        public void OrTakesMaxBetweenTerms(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = AVar.AddTriangularSet("BS", 5, 16, 50);
            var AorB = new FzOr(ASet, BSet);


            //act
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");
            var expected = Math.Max(ASet.GetDom(), BSet.GetDom());
            var actual = AorB.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }

    }
}
