using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源加载系统
/// </summary>
public class GameResourcesSystem : MonoBehaviour
{
    private static Dictionary<string, Object> resDict = new Dictionary<string, Object>();

    public static T GetResources<T>(string resPath) where T : Object
    {
        if (resDict.ContainsKey(resPath))
        {
            return resDict[resPath] as T;
        }
        else
        {
            Object res = Resources.Load(resPath);
            resDict.Add(resPath, res);
            return res as T;
        }

    }
}
