using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class SetLaunchStageToAnimEndCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.GetModel<GameStateModel>().SetLaunchStageToAnimEnd();
    }
}
