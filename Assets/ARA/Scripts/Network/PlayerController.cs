using UnityEngine;
using Unity.Netcode;
using UniRx;
using ARA.Grid;
using ARA.Player;
using ARA.Presenter;
using ARA.Animation;
using ARA.UI;
using ARA.Game;
using Cysharp.Threading.Tasks;

namespace ARA.Network
{
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] private Vector2Int _gridSize;
        [SerializeField] private Vector2Int _initialPosition;

        [SerializeField] private MoveSelectGrid MoveSelectGrid;
        [SerializeField] private InputAnimator InputAnimator;
        [SerializeField] private GridFloatView GridFloatView;
        [SerializeField] private SystemMassageManager Manager;
        [SerializeField] private UIManager _uiManager;

        private GameManager _gameManager;
        private PlayerCore[] _playerCores;
        private int _connectedPlayer;

        private IMoveInputView _moveInputView => MoveSelectGrid;
        private IInputAnimator _inputAnimator => InputAnimator;
        private IGridFloatView _gridFloatView => GridFloatView;
        private IDecidableView _decidableView => _uiManager;

        public override async void OnNetworkSpawn()
        {
        }
    }
}