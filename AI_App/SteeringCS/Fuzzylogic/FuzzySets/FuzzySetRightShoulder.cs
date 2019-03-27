using System;

namespace SteeringCS.Fuzzylogic.FuzzySets
{
    public class FuzzySetRightShoulder : FuzzySet
    {
        //the values that define the shape of this FLV
        private double PeakPoint;
        private double LeftOffset;
        private double RightOffset;

        public FuzzySetRightShoulder(double peak, double lft, double rgt) : base(((peak + rgt) + peak) / 2)
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
            //find DOM if left of center
            if (val <= PeakPoint && val > PeakPoint - LeftOffset)
            {
                double grad = 1.0 / LeftOffset;
                return grad * (val - (PeakPoint - LeftOffset));
            }
            //find DOM if right of center
            if (val > PeakPoint)
            {
                return 1.0;
            }
            //out of range of this FLV, return zero
            return 0.0;
            
        }

    }
}
