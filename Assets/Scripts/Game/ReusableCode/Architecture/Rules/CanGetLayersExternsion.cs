using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��ĳЩ������Է��ʵ�ģ�Ͳ�������չ����
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
