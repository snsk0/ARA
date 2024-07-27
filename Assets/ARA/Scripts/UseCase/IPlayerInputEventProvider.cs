using System;

namespace ARA
{
    public interface IPlayerInputEventProvider
    {
        IObservable<MoveInputResult> MoveInputObservable { get; }
        IObservable<SelectCardResult> SelectCardInputObservable { get; }
        IObservable<PlayerParameter> ResetInputObservable { get; }
    }
}