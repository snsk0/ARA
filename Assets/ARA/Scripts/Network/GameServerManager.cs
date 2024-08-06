using ARA.Player;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ARA.Network
{
    [RequireComponent(typeof(NetworkObject))]
    public class GameServerManager : NetworkBehaviour
    {
        public static GameServerManager Singleton;

        [SerializeField]
        private Vector2Int _gridSize;

        [SerializeField]
        private Vector2Int _initialPosition;

        [SerializeField]
        private uint _playerNumber;

        [SerializeField]
        private GameServerConnector _connector;

        private Dictionary<ulong, PlayerCore> _players = new Dictionary<ulong, PlayerCore>();
        private Dictionary<ulong, GameServerConnector> _connectors = new Dictionary<ulong, GameServerConnector>();

        //サーバーの時のみ生成する
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Singleton = this;
                DontDestroyOnLoad(Singleton);

                NetworkManager.Singleton.OnClientConnectedCallback += ((cliendID) =>
                {
                    //プレイヤーの生成
                    GridField gridField = new GridField(_gridSize);
                    GridTransform movable = new GridTransform(gridField, _initialPosition);
                    PlayerCore player = new PlayerCore(new PlayerParameter(), movable);

                    //Idと紐づけて保存する
                    _players.Add(cliendID, player);
                    _connectors.Add(cliendID, _connector);

                    //プレイヤー数がそろったらConnectorに指示を飛ばす
                    if(_players.Count == _playerNumber)
                    {
                        _connector.InitializeGameRpc(_gridSize, _initialPosition);
                    }
                });
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
