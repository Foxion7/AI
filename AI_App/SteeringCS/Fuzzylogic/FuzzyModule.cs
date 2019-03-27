using System.Collections.Generic;
using SteeringCS.Fuzzylogic.FuzzyTerms;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzyModule
    {
        Dictionary<string, FuzzyVariable> _variables;
        List<FuzzyRule> _rules;
        private int _numSamplesToUseForCentroid = 15;

        public void SetConfidenceToZero() {}

        public FuzzyVariable CreateFLV(string FLVName)
        {
            _variables.Add(FLVName, new FuzzyVariable());
            return _variables[FLVName];
        }

        void addRule(FuzzyTerm reason, FuzzyTerm result)
        {
            _rules.Add(new FuzzyRule(reason, result));
        }

        public void Fuzzify(string NameOfFLV, double val)
        {
            _variables[NameOfFLV].Fuzzify(val);
        }

        public double DeFuzzify(string NameOfFLV, DefuzzifyMethod method = DefuzzifyMethod.centroid, int NumSamples = 15)
        {
            //clear the DOMs of all the consequents
            _rules.ForEach(rule => rule.SetConfidenceOfResultToZero());
            //process the rules
            _rules.ForEach(rule => rule.Calculate());
            //now defuzzify the resultant conclusion using the specified method
            switch (method)
            {
                case DefuzzifyMethod.centroid:
                    return _variables[NameOfFLV].DeFuzzifyCentroid(NumSamples);
                case DefuzzifyMethod.max_av:
                    return _variables[NameOfFLV].DeFuzzifyMaxAv();
            }
            return 0;
        }
    }

    public enum DefuzzifyMethod { max_av, centroid };
}
