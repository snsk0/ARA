using UnityEngine;
using Unity.Netcode;

namespace ARA.Game {
    public struct NetworkResult : INetworkSerializable
    {
        public NetworkResult(Vector2Int playerPosition, Vector2Int enemyPosition, Vector2Int[] playerInputablePositions, Vector2Int[] enemyInputablePositions, bool isPrecedence)
        {
            _playerPosition = playerPosition;
            _enemyPosition = enemyPosition;
            _playerInputablePositions = playerInputablePositions;
            _enemyInputablePositions = enemyInputablePositions;
            _isPrecedence = isPrecedence;
        }

        private Vector2Int _playerPosition;
        private Vector2Int _enemyPosition;
        private Vector2Int[] _playerInputablePositions;
        private Vector2Int[] _enemyInputablePositions;
        private bool _isPrecedence;

        public Vector2Int PlayerPosition => _playerPosition;
        public Vector2Int EnemyPosition => _enemyPosition;
        public Vector2Int[] PlayerInputablePositions => _playerInputablePositions;
        public Vector2Int[] EnemyInputablePositions => _enemyInputablePositions;
        public bool IsPrecedence => _isPrecedence;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _playerPosition);
            serializer.SerializeValue(ref _enemyPosition);
            serializer.SerializeValue(ref _playerInputablePositions);
            serializer.SerializeValue(ref _enemyInputablePositions);
            serializer.SerializeValue(ref _isPrecedence);
        }
    }
}