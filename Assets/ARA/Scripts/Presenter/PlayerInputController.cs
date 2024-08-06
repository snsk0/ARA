using ARA.Player;
using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class PlayerInputController
    {
        public PlayerInputController(InputHandler inputHandler, IMoveInputView view) 
        {
            _inputHandler = inputHandler;
            _view = view;

            _view.ToMoveObservable.Subscribe(position =>
            {
                _inputHandler.MoveInput(position);
            });
        }

        private InputHandler _inputHandler;
        private IMoveInputView _view;
    }
}
