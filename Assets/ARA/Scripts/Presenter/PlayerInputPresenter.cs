using ARA.Player;
using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class PlayerInputPresenter
    {
        public PlayerInputPresenter(InputHandler inputHandler, PlayerCore player, IMoveInputView view, IInputAnimator animator)
        {
            _view = view;
            _animator = animator;

            inputHandler.MoveInputObservable.Subscribe(result =>
            {
                _view.ReceiveInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    _animator.PlayPreMoveAnimation(player.GridTransform.CurrentPosition.Value, result.Input);
                }
            });
        }

        private IMoveInputView _view;
        private IInputAnimator _animator;
    }
}