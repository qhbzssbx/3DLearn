using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace GF
{
    public partial class AssetSystem : ISystem
    {

        public async UniTask<bool> AsyncInit()
        {
            return await InitializeYooAsset();
        }

        private void SetModeToEditorSimulateMode(ResourcePackage package)
        {

        }

        public async UniTask GetScene(string location)
        {
            var sceneMode = UnityEngine.SceneManagement.LoadSceneMode.Single;
            var physicsMode = UnityEngine.SceneManagement.LocalPhysicsMode.None;
            bool suspendLoad = false;
            SceneHandle handle = package.LoadSceneAsync(location, sceneMode, physicsMode, suspendLoad);
            Debug.Log($"Scene name is {handle.SceneName}");
            await handle;
        }




        public void OnDestroy()
        {
            throw new NotImplementedException();
        }
    }

}
