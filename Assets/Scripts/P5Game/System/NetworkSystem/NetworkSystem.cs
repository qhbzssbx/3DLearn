using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Game.Qe.Common.Msg.Proto;
using Google.Protobuf;
using QFramework;
using UnityEngine;

public delegate void MessageHandler<T>(T message) where T : IMessage;

public class NetworkManager : AbstractSystem
{
    TcpManager tcpManager;
    // 消息处理字典，key 是消息 ID，value 是对应的处理方法
    private Dictionary<short, MessageHandler<IMessage>> messageHandlers = new Dictionary<short, MessageHandler<IMessage>>();

    // 注册消息处理函数
    public void RegisterHandler<T>(short messageId, MessageHandler<T> handler) where T : IMessage
    {
        if (messageHandlers.ContainsKey(messageId))
        {
            Debug.LogWarning($"Message ID {messageId} is already registered.");
            return;
        }
        messageHandlers[messageId] = param => handler((T)param);
    }

    // 移除消息处理函数
    public void UnregisterHandler(short messageId)
    {
        if (messageHandlers.ContainsKey(messageId))
        {
            messageHandlers.Remove(messageId);
        }
    }

    // 接收服务器消息
    public void OnReceiveMessage(short messageId, byte[] message)
    {
                LogUtility.Log("!!!!!!   " + messageId);
        if (messageHandlers.TryGetValue(messageId, out var handler))
        {
            if (ProtobufCodeInfo.ProtoCodeToType.TryGetValue(messageId, out var transferFunc))
            {
                var aaa = transferFunc(message);
                handler.Invoke(transferFunc(message));
            }
        }
        else
        {
            Debug.LogWarning($"No handler found for message ID {messageId}");
        }
    }

    // 初始化
    protected override void OnInit()
    {
        // 初始化网络连接（例如 WebSocket、TCP 等）
        tcpManager = TcpManager.Instance;
        tcpManager.CallBack += OnReceiveMessage;
    }
    protected override void OnDeinit()
    {
        tcpManager.CallBack -= OnReceiveMessage;
        tcpManager.Disconnect();
    }

    public void StartConnect()
    {
        tcpManager.Connect("192.168.10.22", 8801);
        Application.quitting += tcpManager.Disconnect;
    }
    public void SendMsg<T>(T message) where T : IMessage, new()
    {
        tcpManager.SendMovement<T>(message);
    }
}

public delegate void ReceiveCallback(short messageId, byte[] t);
public class TcpManager : Singleton<TcpManager>
{
    private TcpClient _tcpClient;
    private NetworkStream _stream;
    private Thread _receiveThread;
    private bool _isConnected = false;


    public event ReceiveCallback CallBack;

    /**
     * 字符最大值
     */
    uint BYTE_X1_MAX_VALUE = Byte.MaxValue;

    /**
     * 2字符最大值
     */
    uint BYTE_X2_MAX_VALUE = UInt16.MaxValue;

    /**
     * 3字符最大值
     */
    uint BYTE_X3_MAX_VALUE = 256 * 256 * 256;

    /**
     * 4字符最大值
     */
    uint BYTE_X4_MAX_VALUE = UInt32.MaxValue;


    /**
     * 协议头+ 消息类型
     */
    private static int HEAD_LENGTH = 1 + 2;

    private static byte HEAD_FLAG1 = 18;
    private static byte HEAD_FLAG2 = 28;
    private static byte HEAD_FLAG3 = 38;
    private static byte HEAD_FLAG4 = 48;

    private TcpManager(){}

    // 连接到服务器（在UI按钮或初始化时调用）
    public void Connect(string ip, int port)
    {
        try
        {
            _tcpClient = new TcpClient();
            _tcpClient.Connect(ip, port);
            _stream = _tcpClient.GetStream();
            _isConnected = true;

            // 启动接收线程
            _receiveThread = new Thread(ReceiveData);
            _receiveThread.Start();
            Debug.LogError("连接成功");
        }
        catch (Exception e)
        {
            Debug.LogError("连接失败: " + e.Message);
        }
        // SOCKET.One().Connect(ip, port);
    }

    // 断开连接
    public void Disconnect()
    {
        _isConnected = false;
        _stream?.Close();
        _tcpClient?.Close();
    }

    public void SendMovement<T>(T t) where T : IMessage, new()
    {
        if (!_isConnected) return;

        // // 序列化并发送
        // byte[] data = ProtoBufferUtility.Serialize(t);
        // Debug.Log("data.length:" + data.Length);

        // _stream.Write(data, 0, data.Length);
        var msgCode = ProtobufCodeInfo.ProtoTypeToCode[typeof(T).Name];
        Send2(msgCode, ProtoBufferUtility.Serialize(t));
    }
    //short sendCount = 0;
    public void Send2(short msgID, byte[] data)
    {
        try
        {

            //byte[] tempFlag = {HEAD_FLAG};
            byte flag = 0;
            int len = data.Length;
            if (len <= BYTE_X1_MAX_VALUE)
            {
                len += 1;
                flag = HEAD_FLAG1;
            }
            else if (len <= BYTE_X2_MAX_VALUE)
            {
                len += 2;
                flag = HEAD_FLAG2;
            }
            else if (len <= BYTE_X3_MAX_VALUE)
            {
                len += 3;
                flag = HEAD_FLAG3;
            }
            else
            {
                len += 4;
                flag = HEAD_FLAG4;
            }


            byte[] byteFlag = { flag };
            byte[] msgid = BitConverter.GetBytes(msgID);
            Array.Reverse(msgid);

            List<byte> headBuf = new List<byte>();



            headBuf.AddRange(CommonUtility.XorByte(byteFlag, HEAD_FLAG1));
            headBuf.AddRange(msgid);

            List<byte> dataBuf = new List<byte>();

            dataBuf.AddRange(headBuf);
            headBuf.Clear();

            byte[] byteLen = BitConverter.GetBytes(data.Length);
            Array.Reverse(byteLen);

            if (flag == HEAD_FLAG1)
            {
                byte[] len1 = new byte[1];
                Array.Copy(byteLen, 3, len1, 0, 1);
                dataBuf.AddRange(len1);
            }
            else if (flag == HEAD_FLAG2)
            {
                byte[] len1 = new byte[2];
                Array.Copy(byteLen, 2, len1, 0, 2);
                dataBuf.AddRange(len1);
            }
            else if (flag == HEAD_FLAG3)
            {
                byte[] len1 = new byte[3];
                Array.Copy(byteLen, 1, len1, 0, 3);
                dataBuf.AddRange(len1); ;
            }
            else
            {
                byte[] len1 = new byte[4];
                Array.Copy(byteLen, 0, len1, 0, 4);
                dataBuf.AddRange(len1); ;
            }

            dataBuf.AddRange(CommonUtility.XorByte(data, data.Length));

            byte[] senByte = dataBuf.ToArray();
            // Debug.LogError(msgID + "===" + senByte.Length);
            // this._socket.BeginSend(senByte, 0, senByte.Length, SocketFlags.None, asyncResult =>
            // {
            //     int resultCount = this._socket.EndSend(asyncResult);
            //     // Debug.LogError(resultCount);
            // }, _socket);
            _stream.Write(senByte, 0, senByte.Length);
        }
        catch (Exception e)
        {

        }
    }

    // 接收线程
    // 接收线程
    private void ReceiveData()
    {
        byte[] byteHead = new byte[HEAD_LENGTH];
        int buffLen = 0;
        while (_isConnected)
        {
            try
            {
                int headLen = _stream.Read(byteHead, 0, HEAD_LENGTH);
                if (headLen >= HEAD_LENGTH)
                {
                    List<byte> recvBuf = new List<byte>();

                    byte[] byteFlag = new byte[1];
                    Array.Copy(byteHead, 0, byteFlag, 0, 1);
                    byteFlag = CommonUtility.XorByte(byteFlag, HEAD_FLAG1);

                    byte[] head = new byte[HEAD_LENGTH];
                    Array.Copy(byteHead, 1, head, 1, HEAD_LENGTH - 1);


                    //Array.Reverse(head, 0, 1);

                    //short flag = BitConverter.ToInt16(byteFlag, 0);
                    //short flag = (short)(((head[0] << 8) | head[1] & 0xff));
                    byte flag = byteFlag[0];
                    short code = (short)(((head[1] << 8) | head[2] & 0xff));



                    int len = 0;
                    int offset = 0;
                    if (flag == HEAD_FLAG1)
                    {
                        byte[] byteLen = new byte[1];
                        _stream.Read(byteLen, 0, 1);

                        len = byteLen[0];
                        offset = 1;
                    }
                    else if (flag == HEAD_FLAG2)
                    {
                        byte[] byteLen = new byte[2];
                        _stream.Read(byteLen, 0, 2);

                        len = (byteLen[0] << 8) | byteLen[1] & 0xff;
                        offset = 2;
                    }
                    else if (flag == HEAD_FLAG3)
                    {
                        byte[] byteLen = new byte[3];
                        _stream.Read(byteLen, 0, 3);
                        len = (int)byteLen[0] << 16 | (int)byteLen[1] << 8 | (int)byteLen[2];
                        offset = 3;
                    }
                    else if (flag == HEAD_FLAG4)
                    {
                        byte[] byteLen = new byte[4];
                        _stream.Read(byteLen, 0, 4);
                        len = (int)byteLen[0] << 24 | (int)byteLen[1] << 16 | (int)byteLen[2] << 8 | (int)byteLen[3];
                        offset = 4;
                    }
                    else
                    {
                        byte[] byteLen = new byte[4];
                        _stream.Read(byteLen, 0, 4);
                        len = (int)byteLen[0] << 24 | (int)byteLen[1] << 16 | (int)byteLen[2] << 8 | (int)byteLen[3];
                        offset = 4;
                    }

                    //int len = (int)head[4] << 24 | (int)head[5] << 16 | (int)head[6] << 8 | (int)head[7];
                    //short len = (short)(((head[3] << 8) | head[4] & 0xff));


                    byte[] body = new byte[len];

                    int bodyLen = _stream.Read(body, 0, len);

                    if (bodyLen >= len)
                    {
                        body = CommonUtility.XorByte(body, len);
                        Debug.Log("接收到消息: " + code);
                        CallBack?.Invoke(code, body);
                    }
                    buffLen = 0;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("接收数据错误: " + e.Message);
                Disconnect();
            }
        }
    }
}
