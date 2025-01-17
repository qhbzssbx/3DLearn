using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override async void Init()
    {
        // RegisterSystem(new AssetSystem());
        RegisterSystem(new UISystem());
        RegisterSystem(new SceneSystem());
        RegisterModel(new PlayerModel());

        bool result = await AssetManager.Instance.InitializeYooAsset(); // 初始化资源
        if (!result)
        {
            // 资源初始化失败
        }
    }
}
