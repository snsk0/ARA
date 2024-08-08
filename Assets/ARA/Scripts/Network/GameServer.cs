using ARA.Player;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace ARA.Network
{
    [RequireComponent(typeof(NetworkObject))]
    public class GameServer : NetworkBehaviour
    {
        public static GameServer Instance;

        [SerializeField]
        private Vector2Int _gridSize;

        [SerializeField]
        private Vector2Int _initialPosition;

        [SerializeField]
        private uint _playerNumber;

        [SerializeField]
        private GameServerConnector _connector;

        private Dictionary<ulong, PlayerCore> _players = new Dictionary<ulong, PlayerCore>();
        private Dictionary<ulong, Vector2Int> _inputDatas = new Dictionary<ulong, Vector2Int>();

        //サーバーの時のみ生成する
        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);

                NetworkManager.Singleton.OnClientConnectedCallback += ((cliendID) =>
                {
                    //プレイヤーの生成
                    GridField gridField = new GridField(_gridSize);
                    GridTransform movable = new GridTransform(gridField, _initialPosition);
                    PlayerCore player = new PlayerCore(new PlayerParameter(), movable);

                    //Idと紐づけて保存する
                    _players.Add(cliendID, player);

                    //プレイヤー数がそろったらConnectorに指示を飛ばす
                    if (_players.Count == _playerNumber)
                    {
                        _connector.InitializeGameRpc(_gridSize, _initialPosition);
                        StartGameLoop();
                    }
                });

                _connector.OnInputEvent += (input, param) =>
                {
                    //Inputを保持する
                    _inputDatas.Add(param.Receive.SenderClientId, input);
                };
            }
            else
            {
                Destroy(this);
            }
        }

        private async void StartGameLoop()
        {
            //ゲームの終了条件
            while (true)
            {
                //inputが集まるのを待機する
                await UniTask.WaitUntil(() => _inputDatas.Count == _playerNumber);

                //inputが集まったら処理をする
                foreach (KeyValuePair<ulong, Vector2Int> inputData in _inputDatas)
                {
                    //プレイヤーを取得
                    PlayerCore player = _players[inputData.Key];

                    //座標更新
                    player.GridTransform.Move(inputData.Value);

                    //結果をコネクターに帰す
                    _connector.ProcessResultRpc(player.GridTransform.CurrentPosition.Value, RpcTarget.Single(inputData.Key, default));
                }

                //inputをクリアする
                _inputDatas.Clear();
            }
        }
    }
}
