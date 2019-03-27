using System;

namespace SteeringCS.Fuzzylogic.FuzzySets
{
    public class FuzzySetLeftShoulder : FuzzySet
    {
        //the values that define the shape of this FLV
        private double PeakPoint;
        private double LeftOffset;
        private double RightOffset;

        public FuzzySetLeftShoulder(double peak, double lft, double rgt) : base(((peak - lft) + peak) / 2)
        {
            PeakPoint = peak;
            LeftOffset = lft;
            RightOffset = rgt;
        }

        public override double CalculateDOM(double val)
        {
            //check for case where the offset may be zero
            if ((Math.Abs(RightOffset) < 0.1 && Math.Abs(val - PeakPoint) < 0.1) ||
                (Math.Abs(LeftOffset) < 0.1 && Math.Abs(val - PeakPoint) < 0.1))
            {
                return 1.0;
            }
            //find DOM if right of center
            if (val >= PeakPoint && val < PeakPoint + RightOffset)
            {
                double grad = 1.0 / -RightOffset;
                return grad * (val - PeakPoint) + 1.0;
            }
            //find DOM if left of center
            if (val < PeakPoint)
            {
                return 1.0;
            }
            //out of range of this FLV, return zero
            return 0.0;
            
        }
    }
}
