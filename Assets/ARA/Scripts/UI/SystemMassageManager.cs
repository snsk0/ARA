using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ARA.UI
{
    public class SystemMassageManager : MonoBehaviour
    {
        [SerializeField]
        private Text _systemMassage;

        [SerializeField]
        private float _spacing;

        private void Awake()
        {
            //layoutGroup‚ð’Ç‰Á
            VerticalLayoutGroup vlayout = gameObject.AddComponent<VerticalLayoutGroup>();

            vlayout.childControlHeight = false;
            vlayout.childControlWidth = false;
            vlayout.childForceExpandHeight = false;
            vlayout.childForceExpandWidth = false;
            vlayout.childScaleHeight = true;
            vlayout.childScaleWidth = true;

            vlayout.spacing = _spacing;

            vlayout.childAlignment = TextAnchor.UpperCenter;
        }

        public void DisplaySystemMassage(string message)
        {
            Text text = Instantiate(_systemMassage);
            text.text = message;

            text.transform.SetParent(transform);
            text.transform.SetAsFirstSibling();

            float endValue = text.rectTransform.localScale.y;
            text.rectTransform.localScale = new Vector2(text.rectTransform.localScale.x, 0);
            text.rectTransform.DOScaleY(endValue, 1.0f);
        }
    }
}
