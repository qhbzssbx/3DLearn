using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 让某些对象可以访问到模型层对象的扩展方法
/// </summary>
public static class CanGetLayersExternsion
{
    public static T GetSystem<T>(this ICanGetSystem self) where T:class, ISystem
    {
        return StartArchitecture.Instance.GetGameArchitecture().GetSystem<T>();
    }
    public static T GetModel<T>(this ICanGetModel self) where T : class, IModel
    {
        return StartArchitecture.Instance.GetGameArchitecture().GetModel<T>();
    }
    public static T GetUtility<T>(this ICanGetUtility self) where T : class, IUtility
    {
        return StartArchitecture.Instance.GetGameArchitecture().GetUtility<T>();
    }
}
