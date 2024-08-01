using DG.Tweening;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.Controllers
{
    public class MoveSelectGrid : MonoBehaviour
    {
        [SerializeField]
        private Button _gridButtonPrefab;

        [SerializeField]
        private Image _gridBackGroundPrefab;

        [SerializeField]
        private float _layoutSpacing;

        private CanvasGroup _canvasGroup;

        private List<Button> _gridButtons;

        private Subject<int> _gridSubject = new Subject<int>();
        public IObservable<int> GridObservable => _gridSubject;

        private void Awake()
        {
            _gridSubject.AddTo(this);
            _gridButtons = new List<Button>();

            Initialized(3, 3);
            Activate(new List<int>{ 0, 3, 4, 6 });
        }

        public void Initialized(int x, int y)
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
                    int buttonIndex = (i * y) + x;

                    //ボタンの配置
                    Button button = Instantiate(_gridButtonPrefab);
                    button.transform.SetParent(hlayout.transform);
                    _gridButtons.Add(button);

                    button.onClick.AddListener(() => { OnButtonClick(button, buttonIndex); });
                }

                //全てのボタンのアクティブを切る
                foreach(Button button in _gridButtons)
                {
                    button.interactable = false;
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

        private void OnButtonClick(Button button, int index)
        {
            //インデックスを発行
            _gridSubject.OnNext(index);

            //ボタンの更新を停止
            button.enabled = false;
            button.GetComponent<Image>().color = button.colors.selectedColor;

            //enableが無効な場合
            foreach (Button otherButton in _gridButtons)
            {
                if (otherButton != button && !otherButton.enabled)
                {
                    otherButton.enabled = true;
                    otherButton.GetComponent<Image>().color = Color.white;
                }
            }
        }

        public void Activate(List<int> indexList)
        {
            //全てのボタンのenableを有効化した上でアクティブを切る
            foreach(Button button in _gridButtons)
            {
                button.GetComponent<Image>().color = Color.white;
                button.enabled = true;
                button.interactable = false;
            }

            foreach(int index in indexList)
            {
                _gridButtons[index].interactable = true;
            }
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _canvasGroup.DOFade(0, 1.0f);
                _canvasGroup.transform.DOMoveX(transform.position.x - 25.0f, 1.0f);
            }
        }
    }
}
