using Unity.Netcode;
using UnityEngine;

namespace ARA.Game
{
    public struct NetworkInput : INetworkSerializable
    {
        public NetworkInput(Vector2Int position, int cardId)
        {
            _position = position;
            _cardId = cardId;
        }

        private Vector2Int _position;
        private int _cardId;

        public Vector2Int Position => _position;
        public int CardId => _cardId;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _position);
            serializer.SerializeValue(ref _cardId);
        }
    }
}
