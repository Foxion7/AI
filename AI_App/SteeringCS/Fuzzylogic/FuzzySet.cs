using System;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzySet
    {

        public double DegreeOfMembership { get; set; }
        protected FuzzySet(double RepVal)
        {
            DegreeOfMembership = 0.0;
            RepresentativeValue = RepVal;
        }

        public void ClearDOM() => DegreeOfMembership = 0.0; 

        protected double RepresentativeValue;
        public double GetRepresentativeVal() => RepresentativeValue;

        public virtual double CalculateDOM(double val) => 0;
       
        //if this fuzzy set is part of a consequent FLV and it is fired by a rule,
        //then this method sets the DOM (in this context, the DOM represents a
        //confidence level) to the maximum of the parameter value or the set's
        //existing m_dDOM value
        public void ORwithDOM(double val) => Math.Max(val, DegreeOfMembership);

    };
}
