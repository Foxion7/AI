namespace SteeringCS.Fuzzylogic.FuzzyTerms
{
    //this is the most primitive term.
    public class FzSet : FuzzyTerm
    {
        public readonly FuzzySet Set;

        public FzSet(FuzzySet set)
        {
            Set = set;
        }

        public override double GetDom() => Set.DegreeOfMembership;

        public override void ClearDOM() => Set.ClearDOM();

        public override void ORwithDOM(double val) => Set.ORwithDOM(val);
    }
}
