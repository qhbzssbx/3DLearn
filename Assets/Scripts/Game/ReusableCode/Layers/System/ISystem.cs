using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 系统层接口
/// </summary>
public interface ISystem : INeedInit,ICanSendCommand,ICanSendEvent,ICanRegistAndUnRegistEvent
{

}
