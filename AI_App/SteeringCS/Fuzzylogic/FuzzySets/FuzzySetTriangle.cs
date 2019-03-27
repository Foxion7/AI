using System;

namespace SteeringCS.Fuzzylogic.FuzzySets
{
    public class FuzzySetTriangle : FuzzySet
    {
        //the values that define the shape of this FLV
        private double _peakPoint;
        private double _leftOffset;
        private double _rightOffset;

        public FuzzySetTriangle(double mid, double lft, double rgt) : base(mid)
        {
            _peakPoint = mid;
            _leftOffset = lft;
            _rightOffset = rgt;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the triangle's left or right offsets are zero
            //(to prevent divide by zero errors below)
            if ((Math.Abs(_rightOffset) < 0.1 && Math.Abs(_peakPoint - val) < 0.1) ||
                (Math.Abs(_leftOffset) < 0.1 && Math.Abs(_peakPoint - val) < 0.1))
            {
                return 1.0;
            }

            //find DOM if left of center
            if ((val <= _peakPoint) && (val >= (_peakPoint - _leftOffset)))
            {
                double grad = 1.0 / _leftOffset;
                return grad * (val - (_peakPoint - _leftOffset));
            }
            //find DOM if right of center
            else if ((val > _peakPoint) && (val < (_peakPoint + _rightOffset)))
            {
                double grad = 1.0 / -_rightOffset;
                return grad * (val - _peakPoint) + 1.0;
            }
            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }
    }
}
