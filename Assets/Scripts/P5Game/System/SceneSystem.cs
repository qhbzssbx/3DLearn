using QFramework;
using UnityEngine;
using YooAsset;

public class SceneSystem : AbstractSystem
{
    AssetHandle currentScene;
    AssetHandle nextScene;
    protected override void OnInit()
    {
        Debug.Log("SceneSystem Init");
    }

    internal void ChangeScene(string v)
    {
    }
}
