using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 事件
/// </summary>
public class EventRegistration : IEventRegistration
{
    public Action<Object> OnEvent = obj => { };
}
