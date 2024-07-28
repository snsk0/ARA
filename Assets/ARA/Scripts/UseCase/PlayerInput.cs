using System.Collections.Generic;

namespace ARA
{
    public struct PlayerInput
    {
        private int _toMoveIndex;
        private bool _isTrash;
        private int _attackCardId;
        private IReadOnlyList<int> _supportCardIds;
        private IReadOnlyList<int> _trashCardIds;

        public int ToMoveIndex => _toMoveIndex;
        public bool IsTrash => _isTrash;
        public int AttackCardId => _attackCardId;
        public IReadOnlyList<int> SupportCardIds => _supportCardIds;
        public IReadOnlyList<int> TrashCardIds => _trashCardIds;
    }
}
