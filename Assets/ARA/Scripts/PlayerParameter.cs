using System.Collections.Generic;

namespace ARA
{
    public struct PlayerParameter
    {
        private int _currentIndex;
        private IReadOnlyList<int> _movableIndexes;
        private IReadOnlyList<int> _cardIds;
    }
}
