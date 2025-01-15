using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GF
{
    /// <summary>
    /// 这个类只做初始化,资源热更,销毁相关操作,加载放在AssetSystem中
    /// </summary>
    public partial class AssetSystem
    {
        public EPlayMode ePlayMode = EPlayMode.EditorSimulateMode;
        protected ResourcePackage package;

        private void Start()
        {
            // InitializeYooAsset();
        }

        public async UniTask<bool> InitializeYooAsset()
        {
            // 初始化资源系统
            YooAssets.Initialize();
            // 获取指定的资源包，如果没有找到会报错
            package = YooAssets.GetPackage("DefaultPackage") ?? YooAssets.CreatePackage("DefaultPackage");
            // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
            YooAssets.SetDefaultPackage(package);

            // StartCoroutine("InitPackage", package);

            return await InitPackage();

        }

        private async UniTask<bool> InitPackage()
        {
            bool status = false;
            if (ePlayMode == EPlayMode.EditorSimulateMode)
            {
                var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                var initParameters = new OfflinePlayModeParameters();
                initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
                var initOperation = package.InitializeAsync(initParameters);
                await initOperation;

                if (initOperation.Status == EOperationStatus.Succeed)
                {
                    Debug.Log("资源包初始化成功！");
                    status = true;
                }
                else
                {
                    Debug.LogError($"资源包初始化失败：{initOperation.Error}");
                    status = false;
                }
            }
            else if (ePlayMode == EPlayMode.OfflinePlayMode)
            {
                var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
                var initParameters = new OfflinePlayModeParameters();
                initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
                var initOperation = package.InitializeAsync(initParameters);
                await initOperation;

                if (initOperation.Status == EOperationStatus.Succeed)
                {
                    Debug.Log("资源包初始化成功！");
                    status = true;
                }
                else
                {
                    Debug.LogError($"资源包初始化失败：{initOperation.Error}");
                    status = false;
                }
            }
            else if (ePlayMode == EPlayMode.HostPlayMode)
            {
                string defaultHostServer = "http://127.0.0.1/CDN/Android/v1.0";
                string fallbackHostServer = "http://127.0.0.1/CDN/Android/v1.0";
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                var cacheFileSystemParams = FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
                var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();

                var initParameters = new HostPlayModeParameters();
                initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
                initParameters.CacheFileSystemParameters = cacheFileSystemParams;
                var initOperation = package.InitializeAsync(initParameters);
                await initOperation;

                if (initOperation.Status == EOperationStatus.Succeed)
                {
                    Debug.Log("资源包初始化成功！");
                    status = true;
                }
                else
                {
                    Debug.LogError($"资源包初始化失败：{initOperation.Error}");
                    status = false;
                }
            }
            else if (ePlayMode == EPlayMode.WebPlayMode)
            {
                string defaultHostServer = "http://127.0.0.1/CDN/Android/v1.0";
                string fallbackHostServer = "http://127.0.0.1/CDN/Android/v1.0";
                IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
                var webServerFileSystemParams = FileSystemParameters.CreateDefaultWebServerFileSystemParameters();
                var webRemoteFileSystemParams = FileSystemParameters.CreateDefaultWebRemoteFileSystemParameters(remoteServices); //支持跨域下载

                var initParameters = new WebPlayModeParameters();
                initParameters.WebServerFileSystemParameters = webServerFileSystemParams;
                initParameters.WebRemoteFileSystemParameters = webRemoteFileSystemParams;

                var initOperation = package.InitializeAsync(initParameters);
                await initOperation;

                if (initOperation.Status == EOperationStatus.Succeed)
                {
                    Debug.Log("资源包初始化成功！");
                    status = true;
                }
                else
                {
                    Debug.LogError($"资源包初始化失败：{initOperation.Error}");
                    status = false;
                }
            }

            return status;
        }
    }
}
