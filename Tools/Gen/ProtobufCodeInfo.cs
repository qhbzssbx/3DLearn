using System;
using System.Collections.Generic;
using Google.Protobuf;
using Game.Qe.Common.Msg.Proto;

public static class ProtobufCodeInfo
{
    public static Dictionary<short, Func<byte[], IMessage>> ProtoCodeToType = new Dictionary<short, Func<byte[], IMessage>>()
    {
        
    };

    public static Dictionary<string, short> ProtoTypeToCode = new Dictionary<string, short>()
    {
        
    };
}