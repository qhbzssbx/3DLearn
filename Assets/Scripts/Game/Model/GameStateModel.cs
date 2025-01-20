using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
enum LaunchStage
{
    None,
    BeInitializing,
    EndOfInitialization,
}
public class GameStateModel : AbstractModel
{
    private const int LAUNCHSTAGETASKCOUNT = 2;
    private LaunchStage launchStage = LaunchStage.None;
    private int launchTaskCompleteIndex = 0;
    protected override void OnInit()
    {
        launchStage = LaunchStage.BeInitializing;
    }

    public void LaunchStageAnimEnd()
    {
        launchTaskCompleteIndex ++;
        Debug.Log("启动动画播放完成");
        // this.SendEvent<LaunchStageChangeToAnimEndEvent>();
        CheckLaunchStageState();
    }

    public void LaunchStageArchitectureEndOfInitialization()
    {
        launchTaskCompleteIndex ++;
        Debug.Log("框架初始化完成");
        CheckLaunchStageState();
    }

    public void CheckLaunchStageState()
    {
        if (launchTaskCompleteIndex > LAUNCHSTAGETASKCOUNT)
        {
            
        }
    }
}
