using System.Collections.Generic;

namespace ARA.Character
{
    public struct CharacterParam
    {
        //
        private CharacterRole _role;
        private int _moveRange;
        private IReadOnlyList<int> _cardIds;

        public CharacterRole Role => _role;
        public int MoveRange => _moveRange;
        public IReadOnlyList<int> CardIds => _cardIds;

    }
}
