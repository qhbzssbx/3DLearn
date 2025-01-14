using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using YooAsset;

public partial class AssetSystem : AbstractSystem
{
    private AssetManager assetManager;
    protected override void OnInit()
    {
        var go = new GameObject("AssetManager");
        var assetManager = go.AddComponent<AssetManager>();
    }
    
    private void SetModeToEditorSimulateMode(ResourcePackage package)
    {

    }

    internal AssetHandle GetScene(string v)
    {
        throw new NotImplementedException();
    }
    protected override void OnDeinit()
    {
        // 场景卸载或游戏退出时，清理资源

    }

    private IEnumerator DestroyPackage()
    {
        // 先销毁资源包
        var package = YooAssets.GetPackage("DefaultPackage");
        DestroyOperation operation = package.DestroyAsync();
        yield return operation;

        // 然后移除资源包
        // if (YooAssets.RemovePackage(package))
        // {
        //     Debug.Log("移除成功！");
        // }
    }
}
