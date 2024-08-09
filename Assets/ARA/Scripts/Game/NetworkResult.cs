using UnityEngine;
using Unity.Netcode;

namespace ARA.Game {
    public struct NetworkResult : INetworkSerializable
    {
        public NetworkResult(Vector2Int playerPosition, Vector2Int enemyPosition)
        {
            _playerPosition = playerPosition;
            _enemyPosition = enemyPosition;
        }

        private Vector2Int _playerPosition;
        private Vector2Int _enemyPosition;

        public Vector2Int PlayerPosition => _playerPosition;
        public Vector2Int EnemyPosition => _enemyPosition;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _playerPosition);
            serializer.SerializeValue(ref _enemyPosition);
        }
    }

}