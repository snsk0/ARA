using UniRx;
using ARA.Character;

namespace ARA.Presenter
{
    public class PlayerPresenter
    {
        public PlayerPresenter(CharacterCore player, ITilePositionInputView inputView, IGridFloatView floatView)
        {
            player.GridTransform.Owner.GridSize.Subscribe(size =>
            {
                inputView.Initialize(size);
                floatView.Initialize(size);
            });

            player.GridTransform.CurrentPosition.Subscribe(position =>
            {
                inputView.UpdateView(position, player.GridTransform.GetMovablePositions());
            });
        }
    }
}
