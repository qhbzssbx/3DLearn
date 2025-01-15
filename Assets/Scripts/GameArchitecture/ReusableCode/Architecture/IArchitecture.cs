using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public interface IArchitecture
{
    void RegistSystem<U>(U instance) where U : ISystem;
    void RegistModel<U>(U instance) where U : IModel;
    void RegistUtility<U>(U instance) where U : IUtility;


    public U GetSystem<U>() where U : class, ISystem;
    public U GetModel<U>() where U : class, IModel;
    public U GetUtility<U>() where U : class, IUtility;

    void RegistEvent<U>(Action<object> onEvent) where U : new();
    void UnRegistEvent<U>(Action<object> onEvent) where U : new();
    void SendEvent<U>(object dataObj) where U : new();
    void SendCommond<U>(object dataObj) where U : ICommand, new();

    UniTask<bool> InitAllModules();
}
