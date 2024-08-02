using System.Collections.Generic;

namespace ARA.Player
{
    public struct PlayerParameter
    {
        //
        private int _moveRange;
        private IReadOnlyList<int> _cardIds;

        public int MoveRange => _moveRange;
        public IReadOnlyList<int> CardIds => _cardIds;
    }
}
