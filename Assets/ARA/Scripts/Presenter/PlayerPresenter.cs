using UniRx;
using ARA.Player;

namespace ARA.Presenter
{
    public class PlayerPresenter
    {
        public PlayerPresenter(PlayerCore player, IMoveInputView inputView, IGridFloatView floatView)
        {
            player.GridMovable.Owner.GridSize.Subscribe(size =>
            {
                inputView.Initialize(size);
                floatView.Initialize(size);
            });

            player.GridMovable.CurrentPosition.Subscribe(position =>
            {
                inputView.UpdateUI(player.GridMovable.CurrentPosition.Value, player.GridMovable.GetMovablePositions());
            });
        }
    }
}
