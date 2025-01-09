using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 命令层接口
/// </summary>
public interface ICommand 
{
    public void Execute(object dataObj);
}
