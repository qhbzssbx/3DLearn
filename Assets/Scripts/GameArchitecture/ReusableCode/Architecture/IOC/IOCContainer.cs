using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
    
    /// <summary>
    /// 所有实例的初始化
    /// </summary>
    public async UniTask<bool> InitAllModules()
    {
        bool result = true;
        List<UniTask<bool>> list = new List<UniTask<bool>>();

        // 遍历字典中的所有实例，调用它们的 AsyncInit 方法
        foreach (var item in instancesDict)
        {
            if (item.Value is INeedAsyncInit asyncInitInstance)
            {
                // 将异步任务加入列表，等待执行
                list.Add(asyncInitInstance.AsyncInit());
            }
            else
            {
                Debug.LogWarning(item.Value.GetType().Name + " 不实现 INeedAsyncInit 接口");
            }
        }

        // 等待所有异步初始化操作完成
        bool[] initResults = await UniTask.WhenAll(list);

        // 根据每个模块的初始化结果，决定最终返回值
        for (int i = 0; i < initResults.Length; i++)
        {
            bool initResult = initResults[i];
            if (!initResult)
            {
                Debug.Log(instancesDict.ElementAt(i).Value.GetType().Name + " 初始化失败");
                result = false;
            }
            else
            {
                Debug.Log(instancesDict.ElementAt(i).Value.GetType().Name + " 初始化成功");
            }
        }

        return result;
    }
}
