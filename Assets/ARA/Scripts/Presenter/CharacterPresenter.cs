using UniRx;
using ARA.Character;

namespace ARA.Presenter
{
    public class CharacterPresenter
    {
        public CharacterPresenter(CharacterCore character, ITilePositionView positionView, IGridFloatView floatView)
        {
            character.GridTransform.Owner.GridSize.Subscribe(size =>
            {
                positionView.Initialize(size);
                floatView.Initialize(size);
            });

            character.GridTransform.CurrentPosition.Subscribe(position =>
            {
                positionView.UpdateView(position, character.GridTransform.GetMovablePositions());
            });
        }
    }
}
