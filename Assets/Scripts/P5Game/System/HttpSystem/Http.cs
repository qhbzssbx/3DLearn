using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.Networking;

public enum HTTP_REQUEST_TYPE
    {
        /// <summary>
        /// GET请求
        /// </summary>
        Get,

        /// <summary>
        /// POST请求
        /// </summary>
        Post,
    }

    /// <summary>
    /// HTTP请求工具
    /// </summary>
    public class HTTP : MonoSingleton<HTTP>
    {
        #region Singleton

        private HTTP()
        {
        }

        #endregion

        #region Send HTTP request

        public bool Request(HTTP_REQUEST_TYPE type, string url, Action<string,string> successResponse, 
            Action<int, string, string, Dictionary<string, string>> errorResponse, 
            Dictionary<string, string> data = null, int timeOut = 10,int contentType = 0)
        {
            if (type == HTTP_REQUEST_TYPE.Post && (data == null || data.Count == 0))
            {
                //#if UNITY_EDITOR
                LogUtility.LogError("使用HTTP的POST方式请求服务器，表单数据不能为空！");  
                //#endif

                return false;
            }
            //if (_Networking)
            //{
            //    LogUtility.Log("HTTP引擎正在请求服务器！");

            //    return false;
            //}
            StartCoroutine(
                _Request(type, url, successResponse, errorResponse, data, timeOut,contentType)
            );

            return true;
        }

#endregion

#region HTTP asynchronous request

        private IEnumerator _Request(HTTP_REQUEST_TYPE type, string url, Action<string,string> successResponse, Action<int, string, string, Dictionary<string, string>> errorResponse, Dictionary<string, string> data, int timeOut,int content_type = 0)
        {
            //#if UNITY_EDITOR
            string debug = "URL地址：" + url + "\n";
            debug += "数据：" + FormatFormData(data) + "\n";
            DateTime debugTime = DateTime.UtcNow;
            //#endif
            //生成请求
            UnityWebRequest engine;
            if (type == HTTP_REQUEST_TYPE.Get)
            {
                engine = UnityWebRequest.Get(url + ((data != null && data.Count != 0) ? "?" + FormatFormData(data) : ""));
            }
            else
            {

                //byte[] myData = System.Text.Encoding.UTF8.GetBytes("test");


                WWWForm form = new WWWForm();

                if(content_type == 1){
                    // engine = new UnityWebRequest(url,UnityWebRequest.kHttpVerbPOST);
                    // engine = UnityWebRequest.Post(url, form);
                    form.headers["Content-Type"] = "application/json;charset=utf-8";
                }else{

                }
                foreach (string key in data.Keys)
                {
                    form.AddField(key,data[key]);
                }

                LogUtility.LogError("请求URL：" + url);
            
                if(content_type == 1){
                    engine = new UnityWebRequest(url, "POST");
                    engine.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.Default.GetBytes(LitJson.JsonMapper.ToJson(data)));
                    engine.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                    engine.SetRequestHeader("Content-Type", "application/json");
                }else
                {
                    engine = UnityWebRequest.Post(url, form);
                }
                // engine.uploadHandler.contentType = "application/json";
            }
            
            engine.timeout = timeOut;
            //engine.certificateHandler = new WebRequestCert();
               
            yield return engine.SendWebRequest();

            //#if UNITY_EDITOR
            debug += "消耗时间：" + (DateTime.UtcNow - debugTime).TotalMilliseconds / 1000 + "秒\n";
            //#endif

            //网络问题
            if (engine.isNetworkError)
            {
                //#if UNITY_EDITOR
                LogUtility.Log("网络错误：" + engine.error + "\n" + debug);
                //#endif

                errorResponse(0, engine.error, url, data);

                engine.Dispose();
                yield break;
            }

            //服务器报错
            if (engine.isHttpError)
            {
                //#if UNITY_EDITOR
                debug = "服务器报错（" + engine.responseCode + "）：" + engine.error + "\n" + debug;
                debug += "服务器返回值：" + engine.downloadHandler.text;

                //#endif

                errorResponse((int)engine.responseCode, engine.error, url, data);

                engine.Dispose();
                yield break;
            }

            //请求成功
            //#if UNITY_EDITOR
            LogUtility.Log("请求成功：" + debug + "服务器返回值：" + engine.downloadHandler.text);
            //#endif

            string response = engine.downloadHandler.text;
            engine.Dispose();

            successResponse(response,url);
        }

#endregion

#region Format form data

        /// <summary>
        /// 格式化表单数据
        /// </summary>
        /// <returns>以&拼接的表单数据</returns>
        /// <param name="data">表单原数据</param>
        public string FormatFormData(Dictionary<string, string> data)
        {
            string formData = "";

            if (data == null || data.Count == 0)
            {
                return formData;
            }
            else
            {
                foreach (string k in data.Keys)
                {
                    formData += k + "=" + UnityWebRequest.EscapeURL(data[k]) + "&";
                }

                return formData.Substring(0, formData.Length - 1);
            }
        }

#endregion
    }

