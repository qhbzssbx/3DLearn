using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

/// <summary>
/// 启动动画播放完成通知命令
/// </summary>
public class SetLaunchStageToAnimEndCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.GetModel<GameStateModel>().LaunchStageAnimEnd();
    }
}

/// <summary>
/// 框架初始化完成通知命令
/// </summary>
public class SetLaunchStageArchitectureEndOfInitializationCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        this.GetModel<GameStateModel>().LaunchStageArchitectureEndOfInitialization();
    }
}

/// <summary>
/// 开始热更
/// </summary>
public class StartHotUpdateCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        AssetManager.Instance.ChangeScene();
    }
}
