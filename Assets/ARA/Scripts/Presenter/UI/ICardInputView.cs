using System;

public interface ICardInputView
{
    IObservable<int> InputObservable { get; }
    void ProcessInputResult(int input, bool isSucceeded);
}
