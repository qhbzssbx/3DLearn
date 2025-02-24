using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace P5Game.ResourcesSystem
{
    /// <summary>
    /// 资源加载接口
    /// </summary>
    public interface IResourceLoader
    {
        // /// <summary>
        // /// 加载指定类型的资源
        // /// </summary>
        // T Load<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// 同步加载
        /// </summary>
        public IAssetHandle LoadSync<T>(ResourcePackage package, string path) where T : Object;

        /// <summary>
        /// 异步加载
        /// </summary>
        public UniTask<IAssetHandle> LoadAsync<T>(ResourcePackage package, string path) where T : Object;
    }
    /// <summary>
    /// 资源句柄管理器
    /// </summary>
    public interface IAssetHandle
    {
        GameObject InstantiateGameObject();
        void Release();
    }

    /// <summary>
    /// 默认实现，使用Unity内置的Resources加载方式。
    /// </summary>
    public class DefaultResourceLoader : IResourceLoader
    {
        public UniTask<IAssetHandle> LoadAsync<T>(ResourcePackage package, string path) where T : Object
        {
            throw new System.NotImplementedException();
        }

        public IAssetHandle LoadSync<T>(ResourcePackage package, string path) where T : Object
        {
            var asset = Resources.Load<T>(path);
            if (asset is GameObject go)
                return new DefaultAssetHandle(go);

            throw new System.NotSupportedException($"Default loader only supports GameObject assets. Path: {path}");
        }
    }

    /// <summary>
    /// YooAsset实现
    /// </summary>
    public class YooAssetResourceLoader : IResourceLoader
    {
        /// <summary>
        /// 同步加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="package">YooAsset 资源包</param>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public IAssetHandle LoadSync<T>(ResourcePackage package, string path) where T : Object
        {
            if (package == null || string.IsNullOrEmpty(path))
                throw new System.ArgumentException("Invalid package or path");

            var assetHandle = package.LoadAssetSync<T>(path);
            if (assetHandle.Status != EOperationStatus.Succeed)
                throw new System.IO.FileNotFoundException($"Failed to load asset: {path}");

            return new YooAssetAssetHandle<T>(assetHandle);
        }
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <typeparam name="T">Unity 内置资源类型</typeparam>
        /// <param name="package">YooAsset资源包</param>
        /// <param name="path">资源路径</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>
        public async UniTask<IAssetHandle> LoadAsync<T>(ResourcePackage package, string path) where T : Object
        {
            if (package == null || string.IsNullOrEmpty(path))
            {
                throw new System.ArgumentException("Invalid package or path");
            }

            AssetHandle assetHandle = null;
            try
            {
                assetHandle = package.LoadAssetAsync<T>(path);
                await assetHandle.ToUniTask();

                if (assetHandle.Status == EOperationStatus.Failed)
                {
                    throw new System.IO.FileNotFoundException($"Failed to load asset at path: {path}");
                }

                return new YooAssetAssetHandle<T>(assetHandle);
            }
            catch (System.Exception e)
            {
                assetHandle?.Dispose();
                Debug.LogError($"Load asset failed: {e.Message}");
                throw;
            }
        }
    }

    public class DefaultAssetHandle : IAssetHandle
    {
        public GameObject Go { get; private set; }

        public DefaultAssetHandle(GameObject go)
        {
            Go = go;
        }

        public void Release()
        {
            // GameObject.Destroy(Go);
        }

        public GameObject InstantiateGameObject()
        {
            return GameObject.Instantiate(Go);
        }
    }

    public class YooAssetAssetHandle<T> : IAssetHandle where T : UnityEngine.Object
    {
        private YooAsset.AssetHandle _assetHandle;

        public YooAssetAssetHandle(YooAsset.AssetHandle assetHandle)
        {
            this._assetHandle = assetHandle;
        }

        public GameObject InstantiateGameObject()
        {
            // var go = GameObject.Instantiate(assetHandle.AssetObject as GameObject);
            // go.AddComponent<GameObjectDestroyCallBack>().AddCallBcak(() => { assetHandle.Release(); });
            // return go;
            if (_assetHandle.AssetObject is GameObject original)
            {
                var instance = Object.Instantiate(original);
                instance.AddComponent<GameObjectDestroyCallBack>().AddCallBcak(() => { _assetHandle.Release(); });
                return instance;
            }
            throw new System.InvalidCastException($"Asset is not a GameObject: {_assetHandle.AssetObject?.GetType()}");
        }

        public T GetAsset()
        {
            return _assetHandle.AssetObject as T;
        }

        public void Release()
        {
            if (_assetHandle != null)
            {
                _assetHandle.Release();
            }
            _assetHandle = null;
        }
    }


}
