using UnityEngine;

namespace ARA.InputHandle
{
    public struct InputContainer
    {
        public InputContainer(Vector2Int position)
        {
            Position = position;
        }

        public readonly Vector2Int Position;
    }
}
