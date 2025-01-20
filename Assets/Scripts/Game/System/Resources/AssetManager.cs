using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using YooAsset;
/// <summary>
/// 这个类只做初始化,资源热更,销毁相关操作,加载放在
/// </summary>
public partial class AssetManager : MonoSingleton<AssetManager>
{
    public EPlayMode ePlayMode = EPlayMode.EditorSimulateMode;

    private ResourcePackage package;
    // private Dictionary<string, ResourcePackage> packageDic = new Dictionary<string, ResourcePackage>();

    public async UniTask<bool> InitializeYooAsset()
    {
        // 初始化资源系统
        YooAssets.Initialize();
        // 获取指定的资源包，如果没有找到会报错
        // package = YooAssets.GetPackage("DefaultPackage") ?? YooAssets.CreatePackage("DefaultPackage");
        package = YooAssets.CreatePackage("DefaultPackage");
        // 设置该资源包为默认的资源包，可以使用YooAssets相关加载接口加载该资源包内容。
        YooAssets.SetDefaultPackage(package);

        // StartCoroutine("InitPackage", package);
        var result = await InitPackage(package);
        // 获取资源版本
        var operation = package.RequestPackageVersionAsync();

        await operation;
        if (operation.Status != EOperationStatus.Succeed)
        {
            // 获取失败
            Debug.LogError(operation.Error);
            return false;
        }
        string packageVersion = operation.PackageVersion;
        Debug.Log($"Updated package Version : {packageVersion}");
        // 更新包Manifest文件
        await package.UpdatePackageManifestAsync(operation.PackageVersion);

        return result;
    }

    private async UniTask<bool> InitPackage(ResourcePackage package)
    {
        InitializationOperation initOperation = null;
        if (ePlayMode == EPlayMode.EditorSimulateMode)
        {
            var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
            initOperation = package.InitializeAsync(initParameters);

        }
        else if (ePlayMode == EPlayMode.OfflinePlayMode)
        {
            var buildinFileSystemParams = FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
            var initParameters = new OfflinePlayModeParameters();
            initParameters.BuildinFileSystemParameters = buildinFileSystemParams;
            initOperation = package.InitializeAsync(initParameters);

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
            initOperation = package.InitializeAsync(initParameters);

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
            initOperation = package.InitializeAsync(initParameters);

        }

        if (initOperation != null)
        {
            await initOperation;

            if (initOperation.Status == EOperationStatus.Succeed)
            {
                Debug.Log("资源包初始化成功！");
                return true;
            }
            else
                Debug.LogError($"资源包初始化失败：{initOperation.Error}");
        }

        return false;

    }

    public void ChangeScene()
    {
        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        string location = "Scenes/Login";
        var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
        var physicsMode = LocalPhysicsMode.None;
        bool suspendLoad = false;
        SceneHandle handle = package.LoadSceneAsync(location, sceneMode, physicsMode, suspendLoad);
        yield return handle;
        Debug.Log($"Scene name is {handle.SceneName}");
    }

}
