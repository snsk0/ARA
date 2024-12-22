using UnityEngine;
using Unity.Netcode;

namespace ARA.Game {
    public struct NetworkResult : INetworkSerializable
    {
        public NetworkResult(bool isFormer, Vector2Int position, Vector2Int[] movablePosition, int[] usaCardIds, int usedCardId, float health)
        {
            _isFormer = isFormer;
            _position = position;
            _movablePositions = movablePosition;
            _usableCardIds = usaCardIds;
            _usedCardId = usedCardId;
            _health = health;
        }

        private bool _isFormer;
        private Vector2Int _position;
        private Vector2Int[] _movablePositions;
        private int[] _usableCardIds;
        private int _usedCardId;
        private float _health;

        public bool IsFormer => _isFormer;
        public Vector2Int Position => _position;
        public Vector2Int[] MovablePositions => _movablePositions;
        public int[] UsableCardIds => _usableCardIds;
        public int UsedCardId => _usedCardId;
        public float Health => _health;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _isFormer);
            serializer.SerializeValue(ref _position);
            serializer.SerializeValue(ref _movablePositions);
            serializer.SerializeValue(ref _usableCardIds);
            serializer.SerializeValue(ref _usedCardId);
            serializer.SerializeValue(ref _health);
        }
    }
}