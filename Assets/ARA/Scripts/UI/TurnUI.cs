using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARA.UI
{
    public class TurnUI : MonoBehaviour, IWaitingInputReceivable
    {
        [SerializeField] private Text _text;
        [SerializeField] private Text _animationText;

        private Tween _tween;

        public void NotfyWaitingInput(bool isWaitingInput)
        {
            gameObject.SetActive(!isWaitingInput);

            if (!isWaitingInput)
            {
                _tween = _animationText.DOText("...", 3f).SetLoops(-1, LoopType.Restart);
            }
            else
            {
                _tween.Kill();
                _tween = null;
            }
        }

        public void SetText(string text)
        {
            if(_tween != null)
            {
                _tween.Kill();
                _tween = null;
            }

            _text.text = text;
        }
    }
}