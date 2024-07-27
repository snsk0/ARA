using System;
using UnityEngine;

namespace ARA
{
    public class PlayerInputPocessor : MonoBehaviour, IPlayerInputProcessor, IPlayerInputEventProvider
    {

        public IObservable<MoveInputResult> MoveInputObservable => throw new NotImplementedException();
        public IObservable<SelectCardResult> SelectCardInputObservable => throw new NotImplementedException();
        public IObservable<PlayerParameter> ResetInputObservable => throw new NotImplementedException();

        public void MoveInput(int index)
        {
        }

        public void SelectCard(SelectActionType actionType, int cardId)
        {
        }

        public void CancelCard(SelectActionType actionType, int cardId)
        {
        }

        public void DecideInput()
        {
        }

        public void ResetInput(PlayerParameter parameter)
        {
        }
    }
}
