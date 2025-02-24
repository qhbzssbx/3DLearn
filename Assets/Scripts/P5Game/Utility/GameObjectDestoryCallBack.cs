using System;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDestroyCallBack : MonoBehaviour
{
    // private Action onDestroyCallBcak { get; set; }
    private HashSet<Action> callbacks = new HashSet<Action>();

    public void AddCallBcak(Action callBack)
    {
        if (!callbacks.Add(callBack))
            Debug.LogError($"OnDestroy callback error: AddCallBcak failed");
    }

    public void RemoveCallBack(Action callBack)
    {
        if (!callbacks.Remove(callBack))
            Debug.LogError($"OnDestroy callback error: RemoveCallBack failed");
    }

    private void OnDestroy()
    {
        try
        {
            foreach (var item in callbacks)
                item?.Invoke();
        }
        catch (Exception ex)
        {
            Debug.LogError($"OnDestroy callback error: {ex}");
        }
        finally
        {
            // 确保释放引用
            callbacks.Clear();
        }

    }
}
