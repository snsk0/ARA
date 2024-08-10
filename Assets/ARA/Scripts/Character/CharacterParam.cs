using System.Collections.Generic;

namespace ARA.Character
{
    public struct CharacterParam
    {
        //
        private int _moveRange;
        private IReadOnlyList<int> _cardIds;

        public int MoveRange => _moveRange;
        public IReadOnlyList<int> CardIds => _cardIds;
    }
}
