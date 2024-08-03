using ARA.Player;
using UniRx;

namespace ARA.Presenter
{
    public class PlayerInputController
    {
        public PlayerInputController(PlayerInputHandler inputHandler, IMoveInputView view) 
        {
            _inputHandler = inputHandler;
            _view = view;

            _view.ToMoveObservable.Subscribe(position =>
            {
                _inputHandler.MoveInput(position);
            });
        }

        private PlayerInputHandler _inputHandler;
        private IMoveInputView _view;
    }
}
