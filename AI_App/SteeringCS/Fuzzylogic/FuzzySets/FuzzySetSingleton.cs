namespace SteeringCS.Fuzzylogic.FuzzySets
{
    public class FuzzySetSingleton : FuzzySet
    {
        private double _rightOffset;
        private double _leftOffset;
        private double _midPoint;

        public FuzzySetSingleton(double mid, double lft, double rgt) : base(mid)
        {
            _midPoint = mid;
            _leftOffset = lft;
            _rightOffset = rgt;
        }

        public override double CalculateDOM(double val)
        {
            if ((val >= _midPoint-_leftOffset) &&
                (val <= _midPoint+_rightOffset) )
            {
                return 1.0;
            }

            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }
}
}
