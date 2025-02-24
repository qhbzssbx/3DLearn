using LitJson;
using P5Game.Utility;
using UnityEngine;
using Cysharp.Threading.Tasks;
using QFramework;
using Game.Qe.Common.Msg.Proto;
using Sirenix.OdinInspector;

namespace P5Game.UI
{
    public partial class LoginPanel : UIPanel
    {
        private void Start()
        {
            Button.onClick.AddListener(() => { Login(); });

            this.GetSystem<NetworkManager>().RegisterHandler<SC_login_msg>(1002, OnLoginRpy);
            this.GetSystem<NetworkManager>().RegisterHandler<SC_role_list_msg>(2002, OnRoleListRpy);
            this.GetSystem<NetworkManager>().RegisterHandler<SC_role_create_msg>(2004, OnCreateRoleRpy);
        }
        public void Login()
        {
            JsonData jsonData = new();
            jsonData["countryCode"] = "86";
            jsonData["phone"] = ipt_account.text;
            jsonData["device"] = SystemInfo.deviceModel;
            jsonData["code"] = "123456";
            HttpUtility.PostAsync(
                "http://192.168.10.230:12001/login/phone",
                jsonData,
                result =>
                {
                    if (result.IsSuccess)
                    {
                        LogUtility.Log(result.Data["data"]["token"].ToString());
                        this.GetSystem<NetworkManager>().StartConnect();
                        CS_login_msg cS_Login_Msg = new();
                        cS_Login_Msg.Token = result.Data["data"]["token"].ToString();
                        this.GetSystem<NetworkManager>().SendMsg<CS_login_msg>(cS_Login_Msg);
                        this.GetSystem<NetworkManager>().SendMsg<CS_login_msg>(cS_Login_Msg);
                    }
                    else
                    {
                        Debug.LogError($"登录失败: {result.Error}");
                    }
                }
            ).Forget(); // 使用 Forget() 触发异步任务
        }

        public void OnLoginRpy(SC_login_msg sC_Login_Msg)
        {
            LogUtility.Log(sC_Login_Msg.Token.ToString());
            CS_role_list_msg cS_role_list_msg = new();
            this.GetSystem<NetworkManager>().SendMsg<CS_role_list_msg>(cS_role_list_msg);
        }
        public void OnRoleListRpy(SC_role_list_msg sC_role_list_msg)
        {
            LogUtility.Log(sC_role_list_msg.ToString());
        }
        [Button("创建角色")]
        public void CreatRole()
        {
            CS_role_create_msg cS_Role_Create_Msg = new();
            cS_Role_Create_Msg.Name = "李勃杰2";
            cS_Role_Create_Msg.Sex = 1;
            cS_Role_Create_Msg.Icon = "xxxxxx";
            this.GetSystem<NetworkManager>().SendMsg<CS_role_create_msg>(cS_Role_Create_Msg);
        }

        public void OnCreateRoleRpy(SC_role_create_msg sC_role_create_msg)
        {
            LogUtility.Log(sC_role_create_msg.ToString());
        }
    }
}