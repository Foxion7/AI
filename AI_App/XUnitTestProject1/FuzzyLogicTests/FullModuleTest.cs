using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SteeringCS.Fuzzylogic;
using SteeringCS.Fuzzylogic.FuzzyTerms;
using Xunit;

namespace XUnitTestProject1.FuzzyLogicTests
{
    public class FullModuleTest
    {
        public FuzzyModule Mod;
        public FuzzyVariable AVar;
        public FuzzyVariable BVar;
        public FuzzyVariable CVar;
        public FzSet as1;
        public FzSet as2;
        public FzSet as3;
        public FzSet bs1;
        public FzSet bs2;
        public FzSet bs3;
        public FzSet cs1;
        public FzSet cs2;
        public FzSet cs3;
        private List<FuzzyTerm> rulesForToCS1;
        private List<FuzzyTerm> rulesForToCS2;
        private List<FuzzyTerm> rulesForToCS3;

        public FullModuleTest()
        {
            Mod = new FuzzyModule();
            AVar = Mod.CreateFLV("AV");
            BVar = Mod.CreateFLV("BV");
            CVar = Mod.CreateFLV("CV");
            //arrange
            as1 = AVar.AddLeftShoulderSet("a1", 0, 20, 40);
            as2 = AVar.AddTriangularSet("a2", 20, 40, 50);
            as3 = AVar.AddRightShoulderSet("a3", 40, 50, 100);

            bs1 = BVar.AddLeftShoulderSet("b1", 0, 30, 40);
            bs2 = BVar.AddTriangularSet("b2", 30, 40, 50);
            bs3 = BVar.AddRightShoulderSet("b3", 40, 50, 100);


            cs1 = CVar.AddTriangularSet("c1", 20, 40, 60);
            cs2 = CVar.AddTriangularSet("c2", 40, 60, 80);
            cs3 = CVar.AddRightShoulderSet("c3", 60, 99, 100);

            rulesForToCS1 = new List<FuzzyTerm>
            {
                new FzAnd(as1, bs2),
                new FzAnd(as1, bs1),
                new FzAnd(as2, bs1)
            };
            rulesForToCS1.ForEach(reason => Mod.addRule(reason, cs1));

            rulesForToCS2 = new List<FuzzyTerm>
            {
                new FzAnd(as3, bs1),
                new FzAnd(as1, bs3),
                new FzAnd(as2, bs2),
            };
            rulesForToCS2.ForEach(reason => Mod.addRule(reason, cs2));

            rulesForToCS3 = new List<FuzzyTerm>
            {
                new FzAnd(as2, bs3),
                new FzAnd(as3, bs3),
                new FzAnd(as3, bs2),
            };
            rulesForToCS3.ForEach(reason => Mod.addRule(reason, cs3));
        }

        [Theory]
        [InlineData(20, 40)]
        [InlineData(30, 35)]
        [InlineData(60, 45)]
        [InlineData(25, 45)]
        [InlineData(45, 35)]
        [InlineData(15, 80)]
        [InlineData(36, 1)]
        [InlineData(76, 59)]
        [InlineData(12, 23)]
        public void ConfidenceOfResult_IsMaximumOfConfidenceOfReasons(int AVal, int BVal)
        {


            //act
            Mod.Fuzzify("AV", AVal);
            Mod.Fuzzify("BV", BVal);
            Mod.DeFuzzify("CV");

            var expectedC1 = rulesForToCS1.Max(term => term.GetDom());
            var expectedC2 = rulesForToCS2.Max(term => term.GetDom());
            var expectedC3 = rulesForToCS3.Max(term => term.GetDom());
            //assert

            Assert.Equal(expectedC1, cs1.GetDom());
            Assert.Equal(expectedC2, cs2.GetDom());
            Assert.Equal(expectedC3, cs3.GetDom());

        }
        [Theory]
        [InlineData(20, 40)]
        [InlineData(30, 35)]
        [InlineData(60, 45)]
        [InlineData(25, 45)]
        [InlineData(45, 35)]
        [InlineData(15, 80)]
        [InlineData(36, 1)]
        [InlineData(76, 59)]
        [InlineData(12, 23)]
        public void ResultOfMaxAv_IsAverageOfAllTermsWithWeightedWithAverage(int AVal, int BVal)
        {


            //act
            Mod.Fuzzify("AV", AVal);
            Mod.Fuzzify("BV", BVal);
            var actual = Mod.DeFuzzify("CV", DefuzzifyMethod.MaxAv);

            var confidenceC1 = rulesForToCS1.Max(term => term.GetDom());
            var valC1 = cs1.GetDom() * cs1.GetRepresentativeVal;
            var confidenceC2 = rulesForToCS2.Max(term => term.GetDom());
            var valC2 = cs2.GetDom() * cs2.GetRepresentativeVal;
            var confidenceC3 = rulesForToCS3.Max(term => term.GetDom());
            var valC3 = cs3.GetDom() * cs3.GetRepresentativeVal;

            var expected = (valC1 + valC2 + valC3) / (confidenceC1 + confidenceC2 + confidenceC3);
            //assert

            Assert.Equal(expected, actual);

        }
    }
}
