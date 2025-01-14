using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class StartGameCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        Game.Interface.GetSystem<SceneSystem>().ChangeScene("Login");
    }
} 
