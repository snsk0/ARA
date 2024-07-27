namespace ARA
{
    public struct SelectCardResult
    {
        private SelectActionType _actionType;
        private int _cardId;

        public SelectActionType ActionType => _actionType;
        public int CardId => _cardId;

        private bool _isSuccess;
        public bool IsSuccess => _isSuccess;

        public SelectCardResult(SelectActionType actionType, int cardId, bool isSuccess)
        {
            _actionType = actionType;
            _cardId = cardId;
            _isSuccess = isSuccess;
        }
    }
}