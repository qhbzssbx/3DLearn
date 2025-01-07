using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 游戏架构接口
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Architecture<T> : IArchitecture where T : new()
{
    private IOCContainer iOCContainer = new IOCContainer();
    private GameEventSystem gameEventSystem = new GameEventSystem();

    
    public Architecture() {Init();}

    protected abstract void Init();
}

