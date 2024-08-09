using UniRx;
using ARA.InputHandle;

namespace ARA.Presenter
{
    public class PlayerInputController
    {
        public PlayerInputController(InputHandler inputHandler, ITilePositionInputView view) 
        {
            view.InputObservable.Subscribe(position =>
            {
                inputHandler.MoveInput(position);
            });
        }
    }
}
