using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ARA.Presenter;

namespace ARA.UI
{
    public class PlayerTileInputManager : MonoBehaviour, ITilePositionInputView, IWaitingInputReceivable
    {
        [SerializeField] private TileViewGenerator _generator;
        [SerializeField] private PlayerTileButton _moveSelectButton;

        ///イベント発行用
        private BehaviourSubject<Vector2Int> _inputSubject = new BehaviourSubject<Vector2Int>();
        public IObservable<Vector2Int> InputObservable => _inputSubject;

        //キャンバス一括管理
        private CanvasGroup _canvasGroup;

        //各タイル管理
        private PlayerTileButton _selectedButtonTemp;
        private PlayerTileButton _currentButtonTemp;
        private Dictionary<Vector2Int, PlayerTileButton> _selectButtons = new Dictionary<Vector2Int, PlayerTileButton>();

        private void Awake()
        {
            _inputSubject.AddTo(this);
        }

        public void Initialize(Vector2Int size)
        {
            //キャンバスグループを追加
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            //generatorから生成を行う
            _selectButtons = _generator.Generate(GetComponent<RectTransform>(), _moveSelectButton, size);

            //イベント登録
            foreach(KeyValuePair<Vector2Int, PlayerTileButton> pair in _selectButtons)
            {
                pair.Value.OnClickObservable.Subscribe(_ =>
                {
                    _inputSubject.OnNext(pair.Key);
                });
            }
        }

        public void UpdateView(Vector2Int currentPosition, IReadOnlyList<Vector2Int> movablePositions)
        {
            if(_currentButtonTemp != null)
            {
                _currentButtonTemp.SelectCurrent(false);
            }

            foreach (Vector2Int position in _selectButtons.Keys)
            {
                bool isInteractable = movablePositions.Contains(position);
                _selectButtons[position].SetInteractable(isInteractable);
            }

            if(_selectedButtonTemp != null)
            {
                _selectedButtonTemp.CanselReaction();
            }
            _selectedButtonTemp = _selectButtons[currentPosition];
            _selectedButtonTemp.SelectedReaction();

            _currentButtonTemp = _selectedButtonTemp;
            _currentButtonTemp.SelectCurrent(true);
        }

        public void ProcessInputResult(Vector2Int inputedPosition, bool isSucceeded)
        {
            if (isSucceeded)
            {
                PlayerTileButton button = _selectButtons[inputedPosition];

                _selectedButtonTemp.CanselReaction();
                _selectedButtonTemp = button;
                _selectedButtonTemp.SelectedReaction();
            }
            else
            {
                _selectButtons[inputedPosition].FailedReaction();
            }
        }

        public void NotfyWaitingInput(bool isWaitingInput)
        {
            _canvasGroup.blocksRaycasts = isWaitingInput;
        }

    }
}
