using System;
using System.Collections;
using QFramework;
using UnityEngine;
using YooAsset;

namespace P5Game.Asset
{
    public partial class AssetManager : ICanSendEvent
    {
        #region 下载热更资源      
        IEnumerator Download()
        {
            int downloadingMaxNum = 10;
            int failedTryAgain = 3;
            var package = YooAssets.GetPackage("DefaultPackage");
            var downloader = package.CreateResourceDownloader(downloadingMaxNum, failedTryAgain);
            //没有需要下载的资源         
            if (downloader.TotalDownloadCount == 0)
            {
                yield break;
            }          //需要下载的文件总数和总大小         
            int totalDownloadCount = downloader.TotalDownloadCount;
            long totalDownloadBytes = downloader.TotalDownloadBytes;
            //注册回调方法
            downloader.DownloadErrorCallback = OnDownloadErrorCallback; // 下载发生错误
            downloader.DownloadUpdateCallback = OnDownloadUpdateCallback; // 下载进度发生变化
                                                                          // downloader.OnDownloadOverCallback = OnDownloadOverFunction;
            downloader.DownloadFileBeginCallback = OnDownloadFileBeginCallback; // 开始下载某文件
            downloader.DownloadFinishCallback = OnDownloadFinishCallback; // 下载结束, 无论失败成功
                                                                          //开启下载         
            downloader.BeginDownload();
            yield return downloader;
            //检测下载结果         
            if (downloader.Status == EOperationStatus.Succeed)
            {
                //下载成功
                Debug.Log("更新完成");
            }
            else
            {
                //下载失败
                Debug.Log("更新失败");
            }
        }

        /// <summary>
        /// 下载结束, 无论成功失败
        /// </summary>
        /// <param name="data"></param>
        private void OnDownloadFinishCallback(DownloaderFinishData data)
        {
            if (data.Succeed)
                Debug.Log(string.Format("资源包: {0} 下载成功", data.PackageName));
            else
                Debug.LogError(string.Format("资源包: {0} 下载成失败", data.PackageName));
        }
        /// <summary>
        /// 开始下载某文件
        /// </summary>
        /// <param name="data"></param>
        private void OnDownloadFileBeginCallback(DownloadFileData data)
        {
            Debug.Log(string.Format("开始下载 资源包: {0} 下文件: {1} 大小: {2}", data.PackageName, data.FileName, data.FileSize));
        }
        /// <summary>
        /// 下载进度发生变化
        /// </summary>
        /// <param name="data"></param>
        private void OnDownloadUpdateCallback(DownloadUpdateData data)
        {
            // throw new NotImplementedException();
            HotUpdateProgressChangeEvent hotUpdateProgressChangeEvent = new HotUpdateProgressChangeEvent();
            hotUpdateProgressChangeEvent.PackageName = data.PackageName;
            hotUpdateProgressChangeEvent.Progress = data.Progress;
            hotUpdateProgressChangeEvent.TotalDownloadCount = data.TotalDownloadCount;
            hotUpdateProgressChangeEvent.CurrentDownloadCount = data.CurrentDownloadCount;
            hotUpdateProgressChangeEvent.TotalDownloadBytes = data.TotalDownloadBytes;
            hotUpdateProgressChangeEvent.CurrentDownloadBytes = data.CurrentDownloadBytes;

            this.SendEvent<HotUpdateProgressChangeEvent>(hotUpdateProgressChangeEvent);
        }
        /// <summary>
        /// 下载发生错误
        /// </summary>
        /// <param name="data"></param>
        private void OnDownloadErrorCallback(DownloadErrorData data)
        {
            Debug.Log(string.Format("下载出错：包名：{0}，文件名：{1}，错误信息：{2}", data.PackageName, data.FileName, data.ErrorInfo));
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }

        #endregion
    }
}
