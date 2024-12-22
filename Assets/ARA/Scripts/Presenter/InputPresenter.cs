using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class InputPresenter
    {
        public InputPresenter(InputHandler inputHandler,
            ITilePositionInputView moveInputView, IDecideInputView decideInputView, IInputAnimator animator, IWaitingInputReceivable[] interactableViews)
        {
            //���͊J�n��Ԃ̎󂯓n��
            inputHandler.IsInputWaiting.Subscribe(isInputWaiting =>
            {
                foreach (IWaitingInputReceivable interactableView in interactableViews)
                {
                    interactableView.NotfyWaitingInput(isInputWaiting);
                }

                animator.UpdateFromPosition(inputHandler.TilePositionCashe);
            });

            //���͂�n��
            moveInputView.InputObservable.Subscribe(position =>
            {
                inputHandler.TilePositionInput(position);
            });

            decideInputView.InputObservable.Subscribe(_ =>
            {
                inputHandler.DecideInput();
            });

            //���ʂ�Ԃ�
            inputHandler.TilePositionInputObservable.Subscribe(result =>
            {
                moveInputView.ProcessInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    animator.PlayPreMoveAnimation(result.Input);
                }
            });

            //����\��Ԃ̕R�Â�
            inputHandler.IsDecidable.Subscribe(isDecidable =>
            {
                decideInputView.SetDesidable(isDecidable);
            });
        }
    }
}