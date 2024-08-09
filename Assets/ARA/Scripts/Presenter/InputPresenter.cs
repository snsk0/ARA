using ARA.Character;
using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class InputPresenter
    {
        public InputPresenter(InputHandler inputHandler, CharacterCore player, ITilePositionInputView view, IInputAnimator animator)
        {
            view.InputObservable.Subscribe(position =>
            {
                inputHandler.MoveInput(position);
            });

            inputHandler.MoveInputObservable.Subscribe(result =>
            {
                view.ProcessInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    animator.PlayPreMoveAnimation(player.GridTransform.CurrentPosition.Value, result.Input);
                }
            });
        }
    }
}