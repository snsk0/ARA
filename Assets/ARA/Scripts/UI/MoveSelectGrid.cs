using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using DG.Tweening;
using ARA.Presenter;

namespace ARA.UI
{
    public class MoveSelectGrid : MonoBehaviour, IMoveInputView
    {
        [SerializeField]
        private MoveSelectButton _gridButtonPrefab;

        [SerializeField]
        private Image _gridBackGroundPrefab;

        [SerializeField]
        private float _layoutSpacing;

        private BehaviourSubject<Vector2Int> _gridSubject = new BehaviourSubject<Vector2Int>();
        public IObservable<Vector2Int> ToMoveObservable => _gridSubject;

        private CanvasGroup _canvasGroup;

        private bool _isActive;

        private MoveSelectButton _selectedButtonTemp;
        private Dictionary<Vector2Int, MoveSelectButton> _selectButtons = new Dictionary<Vector2Int, MoveSelectButton>();

        private void Awake()
        {
            _gridSubject.AddTo(this);
        }

        public void Initialize(Vector2Int gridSize)
        {
            //キャンバスグループを追加
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            //ボタンのオーナーを作成
            RectTransform buttonsOwner = new GameObject("ButtonOwner").AddComponent<RectTransform>();
            buttonsOwner.transform.SetParent(transform);
            buttonsOwner.sizeDelta = Vector2.zero;
            buttonsOwner.localPosition = Vector2.zero;

            //垂直Layoutの生成
            VerticalLayoutGroup vlayout = buttonsOwner.gameObject.AddComponent<VerticalLayoutGroup>();

            //layoutの設定
            vlayout.childControlHeight = true;
            vlayout.childControlWidth = true;
            vlayout.childForceExpandHeight = false;
            vlayout.childForceExpandWidth = false;
            vlayout.spacing = _layoutSpacing;

            int x = gridSize.x;
            int y = gridSize.y;

            for (int i = 0; i < y; i++)
            {
                //水平Layoutの生成
                HorizontalLayoutGroup hlayout = new GameObject("Horizontal").AddComponent<HorizontalLayoutGroup>();
                hlayout.transform.SetParent(buttonsOwner.transform);

                //kayoutの設定
                hlayout.childControlHeight = false;
                hlayout.childControlWidth = false;
                hlayout.childForceExpandHeight = false;
                hlayout.childForceExpandWidth = false;
                hlayout.childScaleHeight = true;
                hlayout.childScaleWidth = true;
                hlayout.spacing = _layoutSpacing;

                //ボタンの生成
                for (int j = 0; j < x; j++)
                {
                    //ボタン座標の取得
                    Vector2Int position = new Vector2Int(j, i);

                    //ボタンの配置
                    MoveSelectButton button = Instantiate(_gridButtonPrefab);
                    button.transform.SetParent(hlayout.transform);
                    _selectButtons.Add(position, button);

                    //イベント登録
                    button.OnClickObservable.Subscribe(unit => { _gridSubject.OnNext(position); }).AddTo(this);
                }
            }

            //BackGroundを追加
            Image backGround = Instantiate(_gridBackGroundPrefab.gameObject, gameObject.transform).GetComponent<Image>();
            backGround.transform.SetAsLastSibling();

            //サイズを拡張
            Vector2 baseSize = _gridButtonPrefab.GetComponent<RectTransform>().sizeDelta;
            backGround.rectTransform.sizeDelta = new Vector2(baseSize.x * x + _layoutSpacing * (x + 1), baseSize.y * y + _layoutSpacing * (y + 1));

            //座標を調整
            Vector2 prePosition = GetComponent<RectTransform>().position;
            Vector2 size = backGround.rectTransform.sizeDelta;
            backGround.rectTransform.position = new Vector2(prePosition.x + size.x/2 - _layoutSpacing, prePosition.y - size.y/2 + _layoutSpacing);
        }

        public void UpdateUI(Dictionary<Vector2Int, bool> isActives, Vector2Int currentPosition)
        {
            foreach(Vector2Int position in _selectButtons.Keys)
            {
                _selectButtons[position].SetActive(isActives[position]);
            }

            _selectedButtonTemp.CanselReaction();
            _selectedButtonTemp = _selectButtons[currentPosition];
            _selectedButtonTemp.SelectedReaction();
        }

        public void SetActive(bool isActive)
        {
            if (isActive && !_isActive)
            {
                _canvasGroup.DOFade(0, 1.0f);
                _canvasGroup.transform.DOMoveX(transform.position.x - 25.0f, 1.0f);
                _canvasGroup.blocksRaycasts = false;

                _isActive = isActive;
            }
            else if(!isActive && _isActive)
            {
                _canvasGroup.DOFade(1.0f, 1.0f);
                _canvasGroup.transform.DOMoveX(transform.position.x + 25.0f, 1.0f);
                _canvasGroup.blocksRaycasts = true;

                _isActive = isActive;
            }
        }

        public void ReceiveInputResult(Vector2Int inputedPosition, bool isSucceeded)
        {
            if (isSucceeded)
            {
                MoveSelectButton button = _selectButtons[inputedPosition];
                _selectedButtonTemp.CanselReaction();
                _selectedButtonTemp = button;
                _selectedButtonTemp.SelectedReaction();
            }
            else
            {
                _selectButtons[inputedPosition].FailedReaction();
            }
        }
    }
}
