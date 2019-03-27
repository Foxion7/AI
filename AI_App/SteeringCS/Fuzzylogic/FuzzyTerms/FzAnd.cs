using System.Linq;

namespace SteeringCS.Fuzzylogic.FuzzyTerms
{
    public class FzAnd : FuzzyTerm
    {
        private FuzzyTerm[] _terms;

        public FzAnd(params FuzzyTerm[] terms)
        {
            _terms = terms;
        }

        public override double GetDom()
        {
            if (_terms.Length < 1)
                return 0;
            return _terms.Min(term => term.GetDom());
        }

        public override void ClearDOM()
        {
            foreach (var fuzzyTerm in _terms)
            {
                fuzzyTerm.ClearDOM();
            }
        }

        public override void ORwithDOM(double val)
        {
            foreach (var fuzzyTerm in _terms)
            {
                fuzzyTerm.ORwithDOM(val);
            }
        }
    }
}
