using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 允许对象异步初始化的接口
/// </summary>
public interface INeedAsyncInit
{
    public Cysharp.Threading.Tasks.UniTask<bool> AsyncInit();
}
