using System.Diagnostics.Contracts;
using TMPro;

namespace ARA.Card
{
    public class CardParameter
    {
        public CardParameter(int power, int fast, RangeType rangeType, int[,] range)
        {
            Power = power;
            Fast = fast;
            RangeType = rangeType;
            Range = range;
        }

        public readonly int Power;
        public readonly int Fast;
        public readonly RangeType RangeType;
        public readonly int[,] Range;
    }
}
