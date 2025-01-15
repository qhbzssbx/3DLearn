// using System.Collections;
// using System.Collections.Generic;
// using Cysharp.Threading.Tasks;
// using UnityEngine;
// using YooAsset;
// /// <summary>
// /// 资源加载系统
// /// </summary>
// public class GameResourcesSystem : MonoBehaviour, ISystem
// {
//     public async UniTask AsyncInit()
//     {
//         YooAssets.Initialize();
//         // 创建默认的资源包
//         var package = YooAssets.CreatePackage("ui");

//     }

//     private IEnumerator DestroyPackage()
//     {
//         // 先销毁资源包
//         var package = YooAssets.GetPackage("ui");
//         DestroyOperation operation = package.DestroyAsync();
//         yield return operation;

//         // 然后移除资源包
//         if (YooAssets.RemovePackage("ui"))
//         {
//             Debug.Log("移除成功！");
//         }
//     }

//     public void OnDestroy()
//     {
//         // 资源系统销毁时移除资源包
//         StartCoroutine(DestroyPackage());
//     }
// }
