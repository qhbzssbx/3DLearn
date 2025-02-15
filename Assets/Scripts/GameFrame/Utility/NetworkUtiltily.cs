using Google.Protobuf;

public static class NetworkUtiltily
{
    public static int socketFlag = 1231;

    public static void RegisterHandler<T>(short messageId, MessageHandler<T> handler) where T : IMessage
    {
        GameArchitecture.Interface.GetSystem<NetworkManager>().RegisterHandler(messageId, handler);
    }
    public static void UnRegisterHandler<T>(short messageId, MessageHandler<T> handler) where T : IMessage
    {
        GameArchitecture.Interface.GetSystem<NetworkManager>().UnregisterHandler(messageId);
    }
    public static void RegisterHandler<T>(MessageHandler<T> handler) where T : IMessage
    {
        short messageId = ProtobufCodeInfo.ProtoTypeToCode[typeof(T).Name];
        GameArchitecture.Interface.GetSystem<NetworkManager>().RegisterHandler(messageId, handler);
    }
    public static void UnRegisterHandler<T>(MessageHandler<T> handler) where T : IMessage
    {
        short messageId = ProtobufCodeInfo.ProtoTypeToCode[typeof(T).Name];
        GameArchitecture.Interface.GetSystem<NetworkManager>().UnregisterHandler(messageId);
    }
}
