using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class InputPresenter
    {
        public InputPresenter(InputHandler inputHandler,
            ITilePositionInputView moveInputView, IDecideInputView decideInputView, IInputAnimator animator, IWaitingInputReceivable[] interactableViews)
        {
            //入力開始状態の受け渡し
            inputHandler.IsInputWaiting.Subscribe(isInputWaiting =>
            {
                foreach (IWaitingInputReceivable interactableView in interactableViews)
                {
                    interactableView.NotfyWaitingInput(isInputWaiting);
                }

                animator.UpdateFromPosition(inputHandler.TilePositionCashe);
            });

            //入力を渡す
            moveInputView.InputObservable.Subscribe(position =>
            {
                inputHandler.TilePositionInput(position);
            });

            decideInputView.InputObservable.Subscribe(_ =>
            {
                inputHandler.DecideInput();
            });

            //結果を返す
            inputHandler.TilePositionInputObservable.Subscribe(result =>
            {
                moveInputView.ProcessInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    animator.PlayPreMoveAnimation(result.Input);
                }
            });

            //決定可能状態の紐づけ
            inputHandler.IsDecidable.Subscribe(isDecidable =>
            {
                decideInputView.SetDesidable(isDecidable);
            });
        }
    }
}