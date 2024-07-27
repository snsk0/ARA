using System;
namespace ARA
{
    public interface IPlayerInputProcessor
    {
        public void MoveInput(int index);
        public void SelectCard(SelectActionType actionType, int cardId);
        public void CancelCard(SelectActionType actionType, int cardId);
        public void DecideInput();
    }
}
