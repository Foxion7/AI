using System;

namespace SteeringCS.Fuzzylogic.FuzzyTerms
{
    public class FzVery : FuzzyTerm
    {
        public readonly FuzzySet Set;

        public FzVery(FzSet set)
        {
            Set = set.Set;
        }

        public override double GetDom() => Math.Pow(Set.DegreeOfMembership,2);

        public override void ClearDOM() => Set.ClearDOM();

        public override void ORwithDOM(double val) => Set.ORwithDOM(val);
    }
}
