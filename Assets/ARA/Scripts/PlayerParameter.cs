using System.Collections.Generic;

namespace ARA
{
    public struct PlayerParameter
    {
        private int _currentIndex;
        private IReadOnlyList<int> _movableIndexes;
        private IReadOnlyList<int> _cardIds;

        public int CurrentIndex => _currentIndex;
        public IReadOnlyList<int> MovableIndexes => _movableIndexes;
        public IReadOnlyList<int> CardIds => _cardIds;
    }
}
