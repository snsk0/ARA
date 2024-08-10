namespace ARA.InputHandle
{
    public struct InputResult<T>
    {
        public InputResult(T input, bool isSucceed)
        {
            _input = input;
            _isSucceed = isSucceed;
        }

        private T _input;
        private bool _isSucceed;

        public T Input => _input;
        public bool IsSucceed => _isSucceed;
    }
}
