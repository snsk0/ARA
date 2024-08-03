using ARA.Player;
using UniRx;

namespace ARA.Presenter
{
    public class PlayerInputPresenter
    {
        public PlayerInputPresenter(PlayerInputHandler inputHandler, IMoveInputView view, IInputAnimator animator)
        {
            _inputHandler = inputHandler;
            _view = view;
            _animator = animator;

            _inputHandler.MoveInputObservable.Subscribe(result =>
            {
                _view.ReceiveInputResult(result.Input, result.IsSucceed);

                if (result.IsSucceed)
                {
                    _animator.PlayPreMoveAnimation(inputHandler.Owner.GridMovable.CurrentPosition.Value, result.Input);
                }
            });
        }

        private PlayerInputHandler _inputHandler;
        private IMoveInputView _view;
        private IInputAnimator _animator;
    }
}