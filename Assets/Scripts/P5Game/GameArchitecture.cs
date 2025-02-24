using Cysharp.Threading.Tasks;
using LitJson;
using P5Game.UI;
using P5Game.Utility;
using QFramework;
using UnityEngine;

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

            RegisterSystem(new CameraSystem());
            RegisterSystem(new UISystem());
            RegisterSystem(new SceneSystem());
            RegisterSystem(new NetworkManager());

            assetInitResult = await AssetManager.Instance.InitializeYooAsset(); // 初始化资源]
            if (!assetInitResult)
            {
                // 资源初始化失败
            }
            else if (assetInitResult)
            {
                CheckVersion();

                
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
            // 请求版本信息
            JsonData jsonData = new JsonData();
            jsonData["channel"] = "test";
            jsonData["platform"] = "Editor";
            jsonData["device"] = SystemInfo.deviceModel;
            jsonData["uuid"] = SystemInfo.deviceUniqueIdentifier;
            HttpUtility.PostAsync(
                "http://192.168.10.230:12001/version/fetch",
                jsonData,
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
                        var appVer = result.Data["data"]["appVersion"];
                        var resVer = result.Data["data"]["resVersion"];
                        
                        if (int.Parse(appVer.ToString()) == GetModel<GameModel>().AppVersion)
                        {
                            if (int.Parse(resVer.ToString()) >= GetModel<GameModel>().ResVersion)
                            {
                                GameArchitecture.Interface.SendCommand<SetLaunchStageArchitectureEndOfInitializationCommand>(new SetLaunchStageArchitectureEndOfInitializationCommand());
                            }
                        }
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

