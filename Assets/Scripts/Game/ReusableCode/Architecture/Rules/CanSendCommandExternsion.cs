using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ���Է����������չ����
/// </summary>
public static class CanSendCommandExternsion
{
    public static void SendCommand<T>(this ICanSendCommand self, object dataObj = null) where T: ICommand, new()
    {
        StartArchitecture.Instance.GetGameArchitecture().SendCommond<T>(dataObj);
    }
}
