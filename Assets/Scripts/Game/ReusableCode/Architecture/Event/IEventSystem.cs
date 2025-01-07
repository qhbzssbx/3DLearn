using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEventSystem
{
    void RegisterEvent<T>(Action<object> onEvent);
    void UnRegisterEvent<T>(Action<object> onEvent);
    void SendEvent<T>(object data) where T : new();
}