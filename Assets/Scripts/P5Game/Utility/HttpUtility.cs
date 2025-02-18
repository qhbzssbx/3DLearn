using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using LitJson;


namespace P5Game.Utility
{
    // HTTP 响应结果封装
    public struct ResponseResult
    {
        public bool IsSuccess;   // 是否成功
        public long StatusCode;  // HTTP 状态码
        public string Error;     // 错误信息
        public JsonData Data;           // 反序列化后的数据
    }

    public class HttpUtility
    {
        // 默认超时时间（秒）
        private const int DefaultTimeout = 60;

        /// <summary>
        /// 发送 POST 请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="data">请求数据（自动序列化为 JSON）</param>
        /// <param name="callback">回调函数</param>
        /// <param name="timeout">超时时间（秒）</param>
        public static async UniTask<ResponseResult> PostAsync(string url, object data, Action<ResponseResult> callback = null, int timeout = DefaultTimeout)
        {
            string jsonData = LitJson.JsonMapper.ToJson(data);
            LogUtility.Log(jsonData);
            return await SendRequestAsync(url, UnityWebRequest.kHttpVerbPOST, jsonData, callback, timeout);
        }

        /// <summary>
        /// 发送 GET 请求
        /// </summary>
        public static async UniTask<ResponseResult> GetAsync(string url, Action<ResponseResult> callback = null, int timeout = DefaultTimeout)
        {
            return await SendRequestAsync(url, UnityWebRequest.kHttpVerbGET, null, callback, timeout);
        }

        // 统一发送请求的私有方法
        private static async UniTask<ResponseResult> SendRequestAsync(string url, string method, string jsonBody, Action<ResponseResult> callback, int timeout)
        {
            var result = new ResponseResult();
            using (UnityWebRequest request = new UnityWebRequest(url, method))
            {
                try
                {
                    // 设置请求头和 Body
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");
                    if (!string.IsNullOrEmpty(jsonBody))
                    {
                        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonBody));
                    }

                    // 设置超时并发送请求
                    request.timeout = timeout;
                    await request.SendWebRequest().ToUniTask();

                    // 处理响应
                    result.StatusCode = request.responseCode;
                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        result.IsSuccess = true;
                        if (!string.IsNullOrEmpty(request.downloadHandler.text))
                        {
                            LogUtility.Log(request.downloadHandler.text);
                            result.Data = LitJson.JsonMapper.ToObject<JsonData>(request.downloadHandler.text);
                        }
                    }
                    else
                    {
                        result.Error = $"{request.error} (Code: {request.responseCode})";
                    }
                }
                catch (Exception e)
                {
                    result.Error = $"Request Failed: {e.Message}";
                }

                // 触发回调
                callback?.Invoke(result);
                return result;
            }
        }
    
    }
}