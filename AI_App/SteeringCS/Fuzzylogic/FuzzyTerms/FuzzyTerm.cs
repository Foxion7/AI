namespace SteeringCS.Fuzzylogic.FuzzyTerms
{
    public abstract class FuzzyTerm
    {
        //retrieves the degree of membership of the term
        public abstract double GetDom();

        //clears the degree of membership of the term
        public abstract void ClearDOM();
        // method for updating the DOM of a consequent when a rule fires
        public abstract void ORwithDOM(double val);
    }
}