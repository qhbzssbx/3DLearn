using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;


namespace P5Game.Command
{
    public class UpdateCameraCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.GetSystem<CameraSystem>().UpdateCarmera();
        }
    }
}
