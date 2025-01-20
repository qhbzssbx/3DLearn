using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using YooAsset;

public class GameArchitecture : Architecture<GameArchitecture>
{
    bool assetInitResult = false;
    protected override async void Init()
    {
        this.RegisterEvent<LaunchStageChangeToAnimEndEvent>(e => {
            LaunchStateChange();
        });

        // RegisterSystem(new AssetSystem());
        RegisterSystem(new UISystem());
        RegisterSystem(new SceneSystem());
        RegisterModel(new PlayerModel());
        RegisterModel(new GameStateModel());
        RegisterModel(new GameModel());

        assetInitResult = await AssetManager.Instance.InitializeYooAsset(); // 初始化资源
        if (!assetInitResult)
        {
            // 资源初始化失败
        }
        else if(assetInitResult)
        {
            GameArchitecture.Interface.SendCommand<SetLaunchStageArchitectureEndOfInitializationCommand>(new SetLaunchStageArchitectureEndOfInitializationCommand());
        }
    }

    private void LaunchStateChange()
    {
        AssetManager.Instance.ChangeScene();
    }
}
