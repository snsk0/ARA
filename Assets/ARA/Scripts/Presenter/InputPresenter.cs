using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class InputPresenter
    {
        public InputPresenter(InputHandler inputHandler,
            ITilePositionInputView moveInputView, IDecideInputView decideInputView, IInputAnimator animator, IWaitingInputReceivable[] interactableViews)
        {
            //“ü—ÍŠJŽnó‘Ô‚ÌŽó‚¯“n‚µ
            inputHandler.IsInputWaiting.Subscribe(isInputWaiting =>
            {
                foreach (IWaitingInputReceivable interactableView in interactableViews)
                {
                    interactableView.NotfyWaitingInput(isInputWaiting);
                }

                animator.UpdateFromPosition(inputHandler.TilePositionCashe);
            });

            //“ü—Í‚ð“n‚·
            moveInputView.InputObservable.Subscribe(position =>
            {
                inputHandler.TilePositionInput(position);
            });

            decideInputView.InputObservable.Subscribe(_ =>
            {
                inputHandler.DecideInput();
            });

            //Œ‹‰Ê‚ð•Ô‚·
            inputHandler.TilePositionInputObservable.Subscribe(result =>
            {
                moveInputView.ProcessInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    animator.PlayPreMoveAnimation(result.Input);
                }
            });

            //Œˆ’è‰Â”\ó‘Ô‚Ì•R‚Ã‚¯
            inputHandler.IsDecidable.Subscribe(isDecidable =>
            {
                decideInputView.SetDesidable(isDecidable);
            });
        }
    }
}