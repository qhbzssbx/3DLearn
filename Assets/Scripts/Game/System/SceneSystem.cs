using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEditor.VersionControl;
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
