using System;
using System.Collections.Generic;
using UnityEngine;

public static class HttpRequest
    {
        private static Dictionary<string, Action<string>> _FinalSuccessCallback = new Dictionary<string, Action<string>>();

        private static Action<int> errCallback;
        
        public static void Send(string url, Action<string> success,
            Dictionary<string, string> data = null,Action<int> errorCallback = null, bool showWait = true)
        {
            _FinalSuccessCallback[url] = success;
            errCallback = errorCallback;
            _Send(
                url,
                _AddCommonData(data),
                showWait
            );
        }

        public static void ClearSend(string url, Action<string> success, Dictionary<string, string> data, bool showWait = true,int contentType = 0){
            _FinalSuccessCallback[url] = success;
            bool ok = HTTP.Instance.Request(HTTP_REQUEST_TYPE.Post, url, _SuccessCallback, _ErrorCallback, data,10,contentType);
        }

		private static void _Send(string url, Dictionary<string, string> data, bool showWait)
        {
            bool ok = HTTP.Instance.Request(HTTP_REQUEST_TYPE.Post, url, _SuccessCallback, _ErrorCallback, data);

            if (ok && showWait)
            {
                //UIManager.Mask<LoadingPanel>(TAGUIPrefab.LoadingPanel, "请稍后......");
                //string tips = url;
                //if (AppConst.ShowLog)
                //    tips = "请稍后" + tips;
                //else
                //    tips = "请稍后";
            }
        }

		/// <summary>
		/// 请求成功的回调方法
		/// </summary>
		/// <param name="json">服务器返回的JSON数据</param>
		private static void _SuccessCallback(string json, string url)
        {
            Action<string> action;
            if (_FinalSuccessCallback.TryGetValue(url, out action))
            {
                _FinalSuccessCallback[url] = null;
                action(json);
                //_FinalSuccessCallback[url](data);
            }
        }

        private static void _ErrorCallback(int code, string content, string url, Dictionary<string, string> data)
        {
            if(errCallback!=null)
                errCallback(code);
            //string tipsData = "";
            //if (code == 0)
            //{
            //    //(WARNING)此处需要给玩家一个Alert界面，让玩家重新发送请求
            //    tipsData = "请检查网络";
            //}
            //else
            //{
            //    tipsData = "服务器错误";
            //}
        }

        private static Dictionary<string , string> _AddCommonData(Dictionary<string , string> data) {
            if (data == null) {
                data = new Dictionary<string , string>();
            }
#if UNITY_ANDROID
            data.Add("platform" , "ANDROID");
#elif UNITY_IOS
            data.Add("platform" , "IOS");
#elif UNITY_WINDOWS
            data.Add("platform" , "WINDOWS");
#elif UNITY_EDITOR
            data.Add("platform" , "EDITOR");
#else
            data.Add("platform" , "UNKNOW");
#endif
            data.Add("device_id" , SystemInfo.deviceUniqueIdentifier);

            return data;
        }
    }