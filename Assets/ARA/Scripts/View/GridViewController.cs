using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.View
{
    public class GridViewController : MonoBehaviour
    {
        [SerializeField]
        private Button _gridButtonPrefab;

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

        //テストコード
        private void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
            {
                Activate(new List<int> { 0, 3, 4, 6 });
            }
        }

        public void Initialized(int x, int y)
        {
            //垂直Layoutの生成
            VerticalLayoutGroup vlayout = gameObject.AddComponent<VerticalLayoutGroup>();

            //layoutの設定
            vlayout.childControlHeight = true;
            vlayout.childControlWidth = true;
            vlayout.childForceExpandHeight = false;
            vlayout.childForceExpandWidth = false;

            for (int i = 0; i < y; i++)
            {
                //水平Layoutの生成
                HorizontalLayoutGroup hlayout = new GameObject("Horizontal").AddComponent<HorizontalLayoutGroup>();
                hlayout.transform.SetParent(gameObject.transform);

                //kayoutの設定
                hlayout.childControlHeight = false;
                hlayout.childControlWidth = false;
                hlayout.childForceExpandHeight = false;
                hlayout.childForceExpandWidth = false;
                hlayout.childScaleHeight = true;
                hlayout.childScaleWidth = true;

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
        }

        private void OnButtonClick(Button button, int index)
        {
            //インデックスを発行
            _gridSubject.OnNext(index);

            //ボタンの更新を停止
            button.enabled = false;
            button.GetComponent<Image>().color = button.colors.selectedColor;

            //enableが有効なものを無効化する
            foreach (Button otherButton in _gridButtons)
            {
                if (otherButton != button && otherButton.enabled && otherButton.interactable)
                {
                    otherButton.enabled = false;
                    otherButton.GetComponent<Image>().color = button.colors.normalColor;
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
    }
}
