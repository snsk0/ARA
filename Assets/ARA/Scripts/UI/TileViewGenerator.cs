using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.UI
{
    public class TileViewGenerator : MonoBehaviour
    {
        [SerializeField] private RectTransform _backgroundPrefab;
        [SerializeField] private float _spacing;
        [SerializeField] private bool _isMirror;

        public Dictionary<Vector2Int, T> Generate<T>(RectTransform context, T tilePrefab, Vector2Int size) where T : MonoBehaviour
        {
            //返す辞書の生成
            Dictionary<Vector2Int, T> result = new Dictionary<Vector2Int, T>();

            //UIのコンテキストを生成
            RectTransform uiContext = new GameObject("UIContext").AddComponent<RectTransform>();
            uiContext.transform.SetParent(context);
            uiContext.sizeDelta = Vector2.zero;
            uiContext.localPosition = Vector2.zero;

            //垂直Layoutの生成
            VerticalLayoutGroup vlayout = uiContext.gameObject.AddComponent<VerticalLayoutGroup>();

            //layoutの設定
            vlayout.childControlHeight = true;
            vlayout.childControlWidth = true;
            vlayout.childForceExpandHeight = false;
            vlayout.childForceExpandWidth = false;
            vlayout.spacing = _spacing;

            int x = size.x;
            int y = size.y;

            for (int i = 0; i < y; i++)
            {
                //水平Layoutの生成
                HorizontalLayoutGroup hlayout = new GameObject("Horizontal").AddComponent<HorizontalLayoutGroup>();
                hlayout.transform.SetParent(uiContext.transform);

                //kayoutの設定
                hlayout.childControlHeight = false;
                hlayout.childControlWidth = false;
                hlayout.childForceExpandHeight = false;
                hlayout.childForceExpandWidth = false;
                hlayout.childScaleHeight = true;
                hlayout.childScaleWidth = true;
                hlayout.spacing = _spacing;

                for (int j = 0; j < x; j++)
                {
                    //タイルの座標を指定通りに生成
                    Vector2Int position = new Vector2Int(j, y - i - 1);
                    if (_isMirror)
                    {
                        position = size - position - new Vector2Int(1, 1);
                    }

                    //タイルの追加
                    T tile = Instantiate(tilePrefab);
                    tile.transform.SetParent(hlayout.transform);
                    result.Add(position, tile.GetComponent<T>());
                }
            }

            //BackGroundを追加
            RectTransform background = Instantiate(_backgroundPrefab, context.transform);
            background.transform.SetAsLastSibling();

            //サイズを拡張
            Vector2 baseSize = tilePrefab.GetComponent<RectTransform>().sizeDelta;
            background.sizeDelta = new Vector2(baseSize.x * x + _spacing * (x + 1), baseSize.y * y + _spacing * (y + 1));

            //座標を調整
            Vector2 prePosition = GetComponent<RectTransform>().position;
            Vector2 backgroundSize = background.sizeDelta;
            background.position = new Vector2(prePosition.x + backgroundSize.x / 2 - _spacing, prePosition.y - backgroundSize.y / 2 + _spacing);

            //結果を返す
            return result;
        }
    }
}
