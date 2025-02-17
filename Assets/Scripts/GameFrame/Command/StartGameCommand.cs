using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace P5Game.Command
{
    public class StartGameCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            GameArchitecture.Interface.GetSystem<SceneSystem>().ChangeScene("Login");
        }
    }
}
