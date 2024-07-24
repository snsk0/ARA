using Cysharp.Threading.Tasks.Triggers;
using System;
using System.Collections.Generic;
using System.Linq;
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

            Initialized(3, 5);
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

                    button.onClick.AddListener(() =>
                    {
                        //インデックスを発行
                        _gridSubject.OnNext(buttonIndex);

                        //ボタンの更新を停止
                        button.enabled = false;

                        //その他のボタンのインタラクトを切る
                        foreach(Button otherButton in _gridButtons)
                        {
                            if(otherButton != button)
                            {
                                otherButton.interactable = false;
                            }
                        }
                    });
                }
            }
        }

        public void Activate(List<int> indexList)
        {

        }
    }
}
