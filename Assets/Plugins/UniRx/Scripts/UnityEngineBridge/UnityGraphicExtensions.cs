// for uGUI(from 4.6)
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5)

using System;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UniRx
{
    public static partial class UnityGraphicExtensions
    {
        public static IObservable<@bool> DirtyLayoutCallbackAsObservable(this Graphic graphic)
        {
            return Observable.Create<@bool>(observer =>
            {
                UnityAction registerHandler = () => observer.OnNext(@bool.Default);
                graphic.RegisterDirtyLayoutCallback(registerHandler);
                return Disposable.Create(() => graphic.UnregisterDirtyLayoutCallback(registerHandler));
            });
        }

        public static IObservable<@bool> DirtyMaterialCallbackAsObservable(this Graphic graphic)
        {
            return Observable.Create<@bool>(observer =>
            {
                UnityAction registerHandler = () => observer.OnNext(@bool.Default);
                graphic.RegisterDirtyMaterialCallback(registerHandler);
                return Disposable.Create(() => graphic.UnregisterDirtyMaterialCallback(registerHandler));
            });
        }

        public static IObservable<@bool> DirtyVerticesCallbackAsObservable(this Graphic graphic)
        {
            return Observable.Create<@bool>(observer =>
            {
                UnityAction registerHandler = () => observer.OnNext(@bool.Default);
                graphic.RegisterDirtyVerticesCallback(registerHandler);
                return Disposable.Create(() => graphic.UnregisterDirtyVerticesCallback(registerHandler));
            });
        }
    }
}

#endif