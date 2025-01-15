using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
/// <summary>
/// UI系统
/// </summary>
public class UISystem : IUISystem
{
    public async UniTask<bool> AsyncInit()
    {
        return true;
    }

    public void OnDestroy()
    {
    }
}
