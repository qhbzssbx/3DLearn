using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OpenPanelCommand : ICommand
{
    public void Execute(object dataObj)
    {
        string str = (string)dataObj;
        Debug.Log("��������ϵͳ " + str);
    }
}
