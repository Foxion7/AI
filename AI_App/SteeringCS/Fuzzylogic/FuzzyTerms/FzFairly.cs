using System;

namespace SteeringCS.Fuzzylogic.FuzzyTerms
{
    public class FzFairly : FuzzyTerm
    {
        public readonly FuzzySet Set;

        public FzFairly(FzSet set)
        {
            Set = set.Set;
        }

        public override double GetDom() => Math.Sqrt(Set.DegreeOfMembership) ;

        public override void ClearDOM() => Set.ClearDOM();

        public override void ORwithDOM(double val) => Set.ORwithDOM(val);
    }
}
