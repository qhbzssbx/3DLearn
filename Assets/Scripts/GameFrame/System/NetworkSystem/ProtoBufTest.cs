// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Net.Sockets;
// using System.Threading;
// using System;
// using System.Linq;
// using Google.Protobuf;
// using UnityEngine.UI;
// using TMPro;
// using Game.Qe.Common.Msg.Proto;
// using Sirenix.OdinInspector;

// public class ProtoBufTest : MonoBehaviour
// {

//     byte[] bytes;
//     TcpManager networkManager;

//     public TMP_Text text;
//     public TMP_InputField tipt1;
//     public TMP_InputField tipt2;
//     public TMP_InputField tipt3;
//     public TMP_InputField tipt4;

//     // 线程安全的队列
//     private Queue<SC_login_msg> _messageQueue = new Queue<SC_login_msg>();
//     private object _queueLock = new object();

//     // Start is called before the first frame update
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {
//         // 在主线程处理队列
//         lock (_queueLock)
//         {
//             while (_messageQueue.Count > 0)
//             {
//                 SC_login_msg data = _messageQueue.Dequeue();
//                 // text.text = FormatUserInfo(data);
//                 Debug.Log("data:" + data);
//             }
//         }
//     }
//     [Button("CreateProto")]
//     public void CreateProto()
//     {
//         //类序列化为二进制
//         CS_login_msg cs_login_msg = new CS_login_msg
//         {
//             Token = "asdasdasdasdasdsa",
//         };

//         networkManager.SendMovement(cs_login_msg);
//     }

//     string FormatUserInfo(SC_login_msg userInfo)
//     {
//         return $"<color=#FFD700><b>{userInfo.PlayerId}</b></color>\n" +
//                $"<color=#FFA500>Gold: {userInfo.Token:N0}</color>\n" +
//                $"<color=#00FFFF>Diamonds: {userInfo.Result:N0}</color>\n";
//     }

//     public void StartServer()
//     {
//         // TcpBroadcastServer.Main();
//         networkManager.Disconnect();
//     }

//     [Button("LinkServer")]
//     public void LinkServer()
//     {
//         networkManager = new TcpManager();
//         networkManager.Connect("192.168.10.22", 8801);
//         networkManager.CallBack += ReceiveData;
//     }

//     public void ReceiveData(SC_login_msg t)
//     {
//         // 将数据加入队列，而非直接调用回调
//         lock (_queueLock)
//         {
//             _messageQueue.Enqueue(t);
//         }
//     }
// }


