using System.Collections.Generic;
using SteeringCS.Fuzzylogic.FuzzyTerms;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzyModule
    {

        private Dictionary<string, FuzzyVariable> _variables;
        private List<FuzzyRule> _rules;
        public int NumSamplesToUseForCentroid { get; set; } = 15;

        public FuzzyModule()
        {
            _rules = new List<FuzzyRule>();
            _variables = new Dictionary<string, FuzzyVariable>();
        }

        public FuzzyVariable CreateFLV(string FLVName)
        {
            _variables.Add(FLVName, new FuzzyVariable());
            return _variables[FLVName];
        }

        public void addRule(FuzzyTerm reason, FuzzyTerm result)
        {
            _rules.Add(new FuzzyRule(reason, result));
        }

        public void Fuzzify(string NameOfFLV, double val)
        {
            _variables[NameOfFLV].Fuzzify(val);
        }

        public double DeFuzzify(string NameOfFLV, DefuzzifyMethod method = DefuzzifyMethod.MaxAv, int NumSamples = 15)
        {
            //clear the DOMs of all the consequents
            _rules.ForEach(rule => rule.SetConfidenceOfResultToZero());
            //process the rules
            _rules.ForEach(rule => rule.Calculate());
            //now defuzzify the resultant conclusion using the specified method
            switch (method)
            {
                case DefuzzifyMethod.Centroid:
                    return _variables[NameOfFLV].DeFuzzifyCentroid(NumSamples);
                case DefuzzifyMethod.MaxAv:
                    return _variables[NameOfFLV].DeFuzzifyMaxAv();
            }
            return 0;
        }
    }

    public enum DefuzzifyMethod { MaxAv, Centroid };
}
