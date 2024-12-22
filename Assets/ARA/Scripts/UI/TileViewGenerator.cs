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
            //�Ԃ������̐���
            Dictionary<Vector2Int, T> result = new Dictionary<Vector2Int, T>();

            //UI�̃R���e�L�X�g�𐶐�
            RectTransform uiContext = new GameObject("UIContext").AddComponent<RectTransform>();
            uiContext.transform.SetParent(context);
            uiContext.sizeDelta = Vector2.zero;
            uiContext.localPosition = Vector2.zero;

            //����Layout�̐���
            VerticalLayoutGroup vlayout = uiContext.gameObject.AddComponent<VerticalLayoutGroup>();

            //layout�̐ݒ�
            vlayout.childControlHeight = true;
            vlayout.childControlWidth = true;
            vlayout.childForceExpandHeight = false;
            vlayout.childForceExpandWidth = false;
            vlayout.spacing = _spacing;

            int x = size.x;
            int y = size.y;

            for (int i = 0; i < y; i++)
            {
                //����Layout�̐���
                HorizontalLayoutGroup hlayout = new GameObject("Horizontal").AddComponent<HorizontalLayoutGroup>();
                hlayout.transform.SetParent(uiContext.transform);

                //kayout�̐ݒ�
                hlayout.childControlHeight = false;
                hlayout.childControlWidth = false;
                hlayout.childForceExpandHeight = false;
                hlayout.childForceExpandWidth = false;
                hlayout.childScaleHeight = true;
                hlayout.childScaleWidth = true;
                hlayout.spacing = _spacing;

                for (int j = 0; j < x; j++)
                {
                    //�^�C���̍��W���w��ʂ�ɐ���
                    Vector2Int position = new Vector2Int(j, y - i - 1);
                    if (_isMirror)
                    {
                        position = size - position - new Vector2Int(1, 1);
                    }

                    //�^�C���̒ǉ�
                    T tile = Instantiate(tilePrefab);
                    tile.transform.SetParent(hlayout.transform);
                    result.Add(position, tile.GetComponent<T>());
                }
            }

            //BackGround��ǉ�
            RectTransform background = Instantiate(_backgroundPrefab, context.transform);
            background.transform.SetAsLastSibling();

            //�T�C�Y���g��
            Vector2 baseSize = tilePrefab.GetComponent<RectTransform>().sizeDelta;
            background.sizeDelta = new Vector2(baseSize.x * x + _spacing * (x + 1), baseSize.y * y + _spacing * (y + 1));

            //���W�𒲐�
            Vector2 prePosition = GetComponent<RectTransform>().position;
            Vector2 backgroundSize = background.sizeDelta;
            background.position = new Vector2(prePosition.x + backgroundSize.x / 2 - _spacing, prePosition.y - backgroundSize.y / 2 + _spacing);

            //���ʂ�Ԃ�
            return result;
        }
    }
}
