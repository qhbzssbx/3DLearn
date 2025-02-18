using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using P5Game.UI;
using P5Game.Utility;
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
                CheckVersion();
                LaunchStateChange();
            });

            // RegisterModel(new PlayerModel());
            RegisterModel(new GameStateModel());
            RegisterModel(new GameModel());
            // RegisterSystem(new AssetSystem());

            RegisterSystem(new CameraSystem());
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

        public class LoginRequest
        {
            public string channel;
            public string platform;
            public string device;
            public string uuid;
        }
        private void CheckVersion()
        {
            // 发送登录请求
            var requestData = new LoginRequest { channel = "test", platform = "Editor", device = SystemInfo.deviceModel, uuid = SystemInfo.deviceUniqueIdentifier};
            HttpUtility.PostAsync(
                "http://192.168.10.22:13001/version/fetch",
                requestData,
                result => 
                {
                    if (result.IsSuccess)
                    {
                        Debug.Log($"登录成功: http output log");
                        Debug.Log($"      code : {result.Data["code"]}");
                        Debug.Log($"      msg : {result.Data["msg"]}");
                        Debug.Log($"      result : {result.Data["data"]["result"]}");
                        Debug.Log($"      appVersion : {result.Data["data"]["appVersion"]}");
                        Debug.Log($"      resVersion : {result.Data["data"]["resVersion"]}");
                        Debug.Log($"      downloadUrl : {result.Data["data"]["downloadUrl"]}");
                        Debug.Log($"      hotfixUrl : {result.Data["data"]["hotfixUrl"]}");
                        Debug.Log($"      publicKey : {result.Data["data"]["publicKey"]}");
                    }
                    else
                    {
                        Debug.LogError($"登录失败: {result.Error}");
                    }
                }
            ).Forget(); // 使用 Forget() 触发异步任务
        }

        private void LaunchStateChange()
        {
            AssetManager.Instance.ChangeScene();
            GetSystem<CameraSystem>().UpdateCarmera();
            GetSystem<UISystem>().OpenUI("LoginPanel");
        }
    }

    
}

