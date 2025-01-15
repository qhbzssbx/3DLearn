using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using QFramework;
using UnityEngine;
using YooAsset;

namespace GF
{
    public class SceneSystem : ISystem
    {
        SceneHandle currentScene;
        SceneHandle nextScene;

        public async UniTaskVoid ChangeScene(string v)
        {
            // await this.GetSystem<AssetSystem>().GetScene(v);
        }

        public async UniTask<bool> AsyncInit()
        {
            return true;
        }

        public void OnDestroy()
        {
            throw new System.NotImplementedException();
        }
    }
}
