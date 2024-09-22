using UnityEngine;

namespace ARA.InputHandle
{
    public struct InputContainer
    {
        public InputContainer(Vector2Int position, int deckIndex)
        {
            Position = position;
            DeckIndex = deckIndex;
        }

        public readonly Vector2Int Position;
        public readonly int DeckIndex;
    }
}
