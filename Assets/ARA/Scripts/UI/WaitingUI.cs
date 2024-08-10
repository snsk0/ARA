using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ARA.UI
{
    public class WaitingUI : MonoBehaviour, IWaitingInputReceivable
    {
        [SerializeField] private Text _text;

        private Tween _tween;

        public void NotfyWaitingInput(bool isWaitingInput)
        {
            gameObject.SetActive(!isWaitingInput);

            if (!isWaitingInput)
            {
                _tween = _text.DOText("...", 3f).SetLoops(-1, LoopType.Restart);
            }
            else
            {
                _tween.Kill();
                _tween = null;
            }
        }
    }
}