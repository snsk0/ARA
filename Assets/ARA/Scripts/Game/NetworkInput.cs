using Unity.Netcode;
using UnityEngine;

namespace ARA.Game
{
    public struct NetworkInput : INetworkSerializable
    {
        public NetworkInput(Vector2Int position, int deckIndex)
        {
            _position = position;
            _deckIndex = deckIndex;
        }

        private Vector2Int _position;
        private int _deckIndex;

        public Vector2Int Position => _position;
        public int DeckIndex => _deckIndex;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _position);
            serializer.SerializeValue(ref _deckIndex);
        }
    }
}
