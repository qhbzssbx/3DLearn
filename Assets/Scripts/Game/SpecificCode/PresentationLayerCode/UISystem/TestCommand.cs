using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct TestCommand : ICommand
{
    public void Execute(object dataObj)
    {
        string str = (string)dataObj;
        Debug.Log("²âÊÔÃüÁîÏµÍ³ " + str);
    }
}
