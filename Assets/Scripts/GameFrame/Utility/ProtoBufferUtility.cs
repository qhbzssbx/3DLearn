using System.Collections;
using System.Collections.Generic;
using Google.Protobuf;
using UnityEngine;

public class ProtoBufferUtility
{
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public static byte[] Serialize(IMessage message)
    {
        return message.ToByteArray();
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="packct"></param>
    /// <returns></returns>
    public static T DeSerialize<T>(byte[] packct) where T : IMessage, new()
    {
        IMessage message = new T();
        try
        {
            return (T)message.Descriptor.Parser.ParseFrom(packct);
        }
        catch (System.Exception e)
        {
            throw e;
        }
    }

}
