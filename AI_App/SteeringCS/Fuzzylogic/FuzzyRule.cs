using SteeringCS.Fuzzylogic.FuzzyTerms;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzyRule
    {
        //antecedent (usually a composite of several fuzzy sets and operators)
        private FuzzyTerm _reason;
        //consequence (usually a single fuzzy set, but can be several ANDed together)
        private FuzzyTerm _result;


        public FuzzyRule(FuzzyTerm reason, FuzzyTerm result)
        {
            _reason = reason;
            _result = result;
        }

        public void SetConfidenceOfResultToZero() { _result.ClearDOM(); }

        //this method updates the DOM (the confidence) of the consequent term with
        //the DOM of the antecedent term.
        public void Calculate()
        {
            _result.ORwithDOM(_reason.GetDom());
        }
    }
}