namespace ARA.Card
{
    public class CardParameter
    {
        public CardParameter(int power, RangeType rangeType, int[,] range)
        {
            Power = power;
            RangeType = rangeType;
            Range = range;
        }

        public readonly int Power;
        public readonly RangeType RangeType;
        public readonly int[,] Range;
    }
}
