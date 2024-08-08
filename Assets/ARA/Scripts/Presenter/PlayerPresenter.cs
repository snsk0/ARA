using UniRx;
using ARA.Player;

namespace ARA.Presenter
{
    public class PlayerPresenter
    {
        public PlayerPresenter(PlayerCore player, IMoveInputView inputView, IGridFloatView floatView)
        {
            player.GridTransform.Owner.GridSize.Subscribe(size =>
            {
                inputView.Initialize(size);
                floatView.Initialize(size);
            });

            player.GridTransform.CurrentPosition.Subscribe(position =>
            {
                inputView.SyncPosition(position, player.GridTransform.GetMovablePositions());
            });
        }
    }
}
