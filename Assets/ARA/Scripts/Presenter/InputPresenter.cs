using ARA.Character;
using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class InputPresenter
    {
        public InputPresenter(InputHandler inputHandler, CharacterCore player,
            ITilePositionInputView moveInputView, IDecideInputView decideInputView, IInputAnimator animator, IWaitingInputReceivable[] interactableViews)
        {
            //“ü—Í‚ð“n‚·
            moveInputView.InputObservable.Subscribe(position =>
            {
                inputHandler.MoveInput(position);
            });

            decideInputView.InputObservable.Subscribe(_ =>
            {
                inputHandler.DecideInput();
            });

            //Œ‹‰Ê‚ð•Ô‚·
            inputHandler.MoveInputObservable.Subscribe(result =>
            {
                moveInputView.ProcessInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    animator.PlayPreMoveAnimation(player.GridTransform.CurrentPosition.Value, result.Input);
                }
            });

            //Œˆ’è‰Â”\ó‘Ô‚Ì•R‚Ã‚¯
            inputHandler.IsDecidable.Subscribe(isDecidable =>
            {
                decideInputView.SetDesidable(isDecidable);
            });

            //“ü—ÍŠJŽnó‘Ô‚ÌŽó‚¯“n‚µ
            inputHandler.IsInputWaiting.Subscribe(isInputWaiting =>
            {
                foreach(IWaitingInputReceivable interactableView in interactableViews)
                {
                    interactableView.NotfyWaitingInput(isInputWaiting);
                }
            });
        }
    }
}