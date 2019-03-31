using System;
using System.Collections.Generic;
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
        public void WithRuleAToB_TheConfidenceInAWillBeTheSameAsInB(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = BVar.AddTriangularSet("BS", 0,10,20);
            var r = Mod.addRule(ASet,BSet);
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");
            //act
            var expected = r.Reason.GetDom();
            var actual = r.Result.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(6)]
        [InlineData(-4)]
        [InlineData(0)]
        public void AndTakesMinBetweenTerms(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = AVar.AddTriangularSet("BS", 0, 10, 20);
            var AandB = new FzAnd(ASet, BSet);
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");
            
            //act
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
        public void OrTakesMaxBetweenTerms(int t)
        {
            //arrange
            var ASet = AVar.AddTriangularSet("AS", 0, 10, 20);
            var BSet = AVar.AddTriangularSet("BS", 0, 10, 20);
            var AorB = new FzOr(ASet, BSet);
            Mod.Fuzzify("AV", t);
            Mod.DeFuzzify("BV");

            //act
            var expected = Math.Min(ASet.GetDom(), BSet.GetDom());
            var actual = AorB.GetDom();

            //assert
            Assert.Equal(expected, actual);
        }
    }
}
