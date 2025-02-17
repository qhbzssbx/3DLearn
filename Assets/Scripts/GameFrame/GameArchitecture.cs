using System.Collections;
using System.Collections.Generic;
using P5Game.UI;
using QFramework;
using UnityEngine;
using YooAsset;

namespace P5Game
{
    public class GameArchitecture : Architecture<GameArchitecture>
    {
        bool assetInitResult = false;
        protected override async void Init()
        {
            this.RegisterEvent<LaunchStageChangeToAnimEndEvent>(e =>
            {
                LaunchStateChange();
            });

            // RegisterModel(new PlayerModel());
            RegisterModel(new GameStateModel());
            RegisterModel(new GameModel());
            // RegisterSystem(new AssetSystem());

            RegisterSystem(new UISystem());
            RegisterSystem(new SceneSystem());
            RegisterSystem(new NetworkManager());


            assetInitResult = await AssetManager.Instance.InitializeYooAsset(); // 初始化资源
            if (!assetInitResult)
            {
                // 资源初始化失败
            }
            else if (assetInitResult)
            {
                GameArchitecture.Interface.SendCommand<SetLaunchStageArchitectureEndOfInitializationCommand>(new SetLaunchStageArchitectureEndOfInitializationCommand());
            }
        }

        private void LaunchStateChange()
        {
            AssetManager.Instance.ChangeScene();
        }
    }
}

