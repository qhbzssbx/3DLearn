using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOCContainer
{
    private Dictionary<Type, object> instancesDict = new Dictionary<Type, object>();

    public void RegisterInstance<T>(T instance)
    {
        Type type = typeof(T);
        if (instancesDict.ContainsKey(type))
        {
            instancesDict[type] = instance;
        }
        else
        {
            instancesDict.Add(type, instance);
        }
    }

    public T GetInstance<T>() where T : class
    {
        Type type = typeof(T);
        object obj = null;
        if (instancesDict.TryGetValue(type, out obj))
        {
            return obj as T; // as操作符，如果转换失败，返回null
        }
        else
        {
            Debug.LogError("IOCContainer: GetInstance failed, type = " + type);
        }
        return null;
    }

    public void InitAllModules()
    {
    }
}
