using SteeringCS.Fuzzylogic.FuzzyTerms;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzyRule
    {
        //antecedent (usually a composite of several fuzzy sets and operators)
        public FuzzyTerm Reason;
        //consequence (usually a single fuzzy set, but can be several ANDed together)
        public FuzzyTerm Result;


        public FuzzyRule(FuzzyTerm reason, FuzzyTerm result)
        {
            Reason = reason;
            Result = result;
        }

        public void SetConfidenceOfResultToZero() { Result.ClearDOM(); }

        //this method updates the DOM (the confidence) of the consequent term with
        //the DOM of the antecedent term.
        public void Calculate()
        {
            Result.ORwithDOM(Reason.GetDom());
        }
    }
}