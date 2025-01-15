using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
/// <summary>
/// 游戏架构接口
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Architecture<T> : IArchitecture where T : new()
{
    private IOCContainer iocContainer = new IOCContainer();
    private GameEventSystem gameEventSystem = new GameEventSystem();

    
    public Architecture() {Init();}

    public async UniTask<bool> InitAllModules()
    {
        return await iocContainer.InitAllModules();
    }

    public void RegistEvent<U>(Action<object> onEvent) where U : new()
    {
        gameEventSystem.RegisterEvent<U>(onEvent);
    }

    public void RegistModel<U>(U instance) where U : IModel
    {
        iocContainer.RegisterInstance<U>(instance);
    }

    public void RegistSystem<U>(U instance) where U : ISystem
    {
        iocContainer.RegisterInstance<U>(instance);
    }

    public void RegistUtility<U>(U instance) where U : IUtility
    {
        iocContainer.RegisterInstance<U>(instance);
    }

    public void SendCommond<U>(object dataObj) where U : ICommand, new()
    {
        var command = new U();
        command.Execute(dataObj);
    }

    public void SendEvent<U>(object dataObj) where U : new()
    {
        gameEventSystem.SendEvent<U>(dataObj);
    }

    public void UnRegistEvent<U>(Action<object> onEvent) where U : new()
    {
        gameEventSystem.UnRegisterEvent<U>(onEvent);
    }

    protected abstract void Init();

    public U GetSystem<U>() where U : class, ISystem
    {
        return iocContainer.GetInstance<U>();
    }
    public U GetModel<U>() where U : class, IModel
    {
        return iocContainer.GetInstance<U>();
    }
    public U GetUtility<U>() where U : class, IUtility
    {
        return iocContainer.GetInstance<U>();
    }
}

