namespace ARA
{
    public struct MoveInputResult
    {
        private int _toMoveIndex;
        private bool _isSuccess;

        public int ToMoveIndex => _toMoveIndex;
        public bool IsSuccess => _isSuccess;

        public MoveInputResult(int toMoveIndex, bool isSuccess)
        {
            _toMoveIndex = toMoveIndex;
            _isSuccess = isSuccess;
        }
    }
}