using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// 可以发送事件的扩展方法
/// </summary>
public static class EventExternsion
{
    public static void SendEvent<T>(this ICanSendEvent self, object data = null) where T : new()
    {
        StartArchitecture.Instance.GetGameArchitecture().SendEvent<T>(data);
    }

    public static void RegistEvent<T>(this ICanRegistAndUnRegistEvent self, Action<object> action) where T : new()
    {
        StartArchitecture.Instance.GetGameArchitecture().RegistEvent<T>(action);
    }
    public static void UnRegistEvent<T>(this ICanRegistAndUnRegistEvent self, Action<object> action) where T : new()
    {
        StartArchitecture.Instance.GetGameArchitecture().UnRegistEvent<T>(action);
    }
}
