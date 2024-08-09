using ARA.Character;
using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class PlayerInputPresenter
    {
        public PlayerInputPresenter(InputHandler inputHandler, CharacterCore player, ITilePositionInputView view, IInputAnimator animator)
        {
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