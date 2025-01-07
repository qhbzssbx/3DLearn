using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : IEventSystem
{
    private Dictionary<Type, IEventRegistration> eventRegistrationDict = new Dictionary<Type, IEventRegistration>();

    public void RegisterEvent<T>(Action<object> onEvent)
    {
        var type = typeof(T);
        IEventRegistration eventRegistration;
        if (eventRegistrationDict.TryGetValue(type, out eventRegistration))
        {
            (eventRegistration as EventRegistration).OnEvent += onEvent;
        }
        else
        {
            eventRegistration = new EventRegistration()
            {
                OnEvent = onEvent
            };
            eventRegistrationDict.Add(type, eventRegistration);
        }
    }

    public void SendEvent<T>(object data) where T : new()
    {
        var type = typeof(T);
        IEventRegistration eventRegistration;
        if (eventRegistrationDict.TryGetValue(type, out eventRegistration))
        {
            (eventRegistration as EventRegistration).OnEvent?.Invoke(data);
        }
    }

    public void UnRegisterEvent<T>(Action<object> onEvent)
    {
        var type = typeof(T);
        IEventRegistration eventRegistration;
        if (eventRegistrationDict.TryGetValue(type, out eventRegistration))
        {
            (eventRegistration as EventRegistration).OnEvent -= onEvent;
        }
    }
}
