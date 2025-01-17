using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
enum LaunchStage
{
    AnimPlaying,
    AnimEnd,
}
public class GameStateModel : AbstractModel
{
    private LaunchStage launchStage = LaunchStage.AnimPlaying;
    protected override void OnInit()
    {
    }

    public void SetLaunchStageToAnimEnd()
    {
        this.launchStage = LaunchStage.AnimEnd;

        this.SendEvent<LaunchStageChangeToAnimEndEvent>();
    }
}
