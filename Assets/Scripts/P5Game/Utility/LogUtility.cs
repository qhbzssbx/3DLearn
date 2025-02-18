using UnityEngine;

public static class  LogUtility
{
    public static void Log(string message, Color color, string message2 = "")
    {
        Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(color) + ">" + message + "</color>" + message2);
    }
    public static void Log(string message)
    {
        Debug.Log(message);
    }
    public static void LogError(string message)
    {
        Debug.LogError(message);
    }
    public static void LogWarning(string message)
    {
        Debug.LogError(message);
    }
}