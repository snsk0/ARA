using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using ARA.Presenter;

namespace ARA.UI
{
    public class MoveSelectGrid : MonoBehaviour, ITilePositionInputView
    {
        [SerializeField] private TileViewGenerator _generator;
        [SerializeField] private MoveSelectButton _moveSelectButton;

        ///イベント発行用
        private BehaviourSubject<Vector2Int> _inputSubject = new BehaviourSubject<Vector2Int>();
        public IObservable<Vector2Int> InputObservable => _inputSubject;

        //キャンバス一括管理
        private CanvasGroup _canvasGroup;

        //各タイル管理
        private MoveSelectButton _selectedButtonTemp;
        private Dictionary<Vector2Int, MoveSelectButton> _selectButtons = new Dictionary<Vector2Int, MoveSelectButton>();

        private Vector2Int _lastPlayerPosition;

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
            foreach(KeyValuePair<Vector2Int, MoveSelectButton> pair in _selectButtons)
            {
                pair.Value.OnClickObservable.Subscribe(_ =>
                {
                    _inputSubject.OnNext(pair.Key);
                });
            }
        }

        public void UpdateView(Vector2Int currentPosition, IReadOnlyList<Vector2Int> movablePositions)
        {
            foreach (Vector2Int position in _selectButtons.Keys)
            {
                bool isInteractable = movablePositions.Contains(position);
                _selectButtons[position].SetInteractable(isInteractable);
                
                if (isInteractable)
                {
                    _selectButtons[position].SetButtonColor(MoveSelectButton.ButtonColor.Movable);
                }
                else
                {
                    _selectButtons[position].SetButtonColor(MoveSelectButton.ButtonColor.UnMovable);
                }
            }

            if(_selectedButtonTemp != null)
            {
                _selectedButtonTemp.CanselReaction();
            }
            _selectedButtonTemp = _selectButtons[currentPosition];
            _selectedButtonTemp.SelectedReaction();

            _lastPlayerPosition = currentPosition;
        }

        public void ProcessInputResult(Vector2Int inputedPosition, bool isSucceeded)
        {
            if (isSucceeded)
            {
                MoveSelectButton button = _selectButtons[inputedPosition];

                _selectedButtonTemp.CanselReaction();
                if (_selectedButtonTemp == _selectButtons[_lastPlayerPosition])
                {
                    _selectedButtonTemp.SetButtonColor(MoveSelectButton.ButtonColor.Current);
                }

                _selectedButtonTemp = button;
                _selectedButtonTemp.SelectedReaction();
            }
            else
            {
                _selectButtons[inputedPosition].FailedReaction();
            }
        }

        public void SetInteractable(bool interactable)
        {
            _canvasGroup.blocksRaycasts = interactable;
        }
    }
}
