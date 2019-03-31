using System;
using System.Collections.Generic;
using System.Linq;
using SteeringCS.Fuzzylogic.FuzzySets;
using SteeringCS.Fuzzylogic.FuzzyTerms;

namespace SteeringCS.Fuzzylogic
{
    public class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> _memberSets;
        private double _minRange;
        private double _maxRange;

        public FuzzyVariable()
        {
            _memberSets = new Dictionary<string, FuzzySet>();
            _minRange = 0.0;
            _maxRange = 0.0;
        }
        private void AdjustRangeToFit(double min, double max)
        {
            if (min < _minRange) _minRange = min;
            if (max > _maxRange) _maxRange = max;
        }

        public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            var set = new FuzzySetLeftShoulder(peak, peak - minBound, maxBound - peak);
            _memberSets.Add(name, set);
            AdjustRangeToFit(minBound, maxBound);
            return new FzSet(set);
        }

        public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            var set = new FuzzySetRightShoulder(peak, peak - minBound, maxBound - peak);
            _memberSets.Add(name, set);
            AdjustRangeToFit(minBound, maxBound);
            return new FzSet(set);
        }

        public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            var set = new FuzzySetTriangle(peak, peak - minBound, maxBound - peak);
            _memberSets.Add(name, set);
            AdjustRangeToFit(minBound, maxBound);
            return new FzSet(set);
        }

        public FzSet AddSingletonSet(string name, double minBound, double peak, double maxBound)
        {
            var set = new FuzzySetSingleton(peak, peak - minBound, maxBound - peak);
            _memberSets.Add(name, set);
            AdjustRangeToFit(minBound, maxBound);
            return new FzSet(set);
        }

        //fuzzify a value by calculating its DOM in each of this variable's subsets
        public void Fuzzify(double val)
        {
            //for each set in the flv calculate the DOM for the given value
            foreach (var curSet in _memberSets)
            {
                curSet.Value.DegreeOfMembership = curSet.Value.CalculateDOM(val);
            }
        }

        //defuzzify the variable using the MaxAv method
        public double DeFuzzifyMaxAv()
        {
            (double bottom, double top) = _memberSets.Values.Aggregate((0.0, 0.0),
                (acc, set) => (acc.Item1 + set.DegreeOfMembership,
                    acc.Item2 + set.GetRepresentativeVal() * set.DegreeOfMembership)
            );

            //make sure bottom is not equal to zero
            if (Math.Abs(bottom) < 0.01) return 0.0;
            return top / bottom;
        }

        //defuzzify the variable using the centroid method
        public double DeFuzzifyCentroid(int NumSamples)
        {
            //calculate the step size
            double StepSize = (_maxRange - _minRange) / NumSamples;

            double TotalArea = 0.0;
            double SumOfMoments = 0.0;

            //step through the range of this variable in increments equal to StepSize
            //adding up the contribution (lower of CalculateDOM or the actual DOM of this
            //variable's fuzzified value) for each subset. This gives an approximation of
            //the total area of the fuzzy manifold.(This is similar to how the area under
            //a curve is calculated using calculus... the heights of lots of 'slices' are
            //summed to give the total area.)
            //
            //in addition the moment of each slice is calculated and summed. Dividing
            //the total area by the sum of the moments gives the centroid. (Just like
            //calculating the center of mass of an object)
            for (int samp = 1; samp <= NumSamples; ++samp)
            {
                //for each set get the contribution to the area. This is the lower of the 
                //value returned from CalculateDOM or the actual DOM of the fuzzified 
                //value itself   
                foreach(var curSet in _memberSets.Values)
                {
                    double contribution = Math.Min(curSet.CalculateDOM(_minRange + samp * StepSize),
                            curSet.DegreeOfMembership);

                    TotalArea += contribution;

                    SumOfMoments += (_minRange + samp * StepSize) * contribution;
                }
            }

            //make sure total area is not equal to zero
            if (Math.Abs(TotalArea) < 0.01) return 0.0;

            return (SumOfMoments / TotalArea);
        }
    }
}