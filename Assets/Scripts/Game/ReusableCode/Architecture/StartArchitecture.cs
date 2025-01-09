using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 游戏架构启动类
/// </summary>
public class StartArchitecture : Singleton<StartArchitecture>, ISingleton
{
    private IArchitecture gameArchitecture;

    /// <summary>
    /// 构造函数
    /// </summary>
    private StartArchitecture()
    {
        Init();
    }

    public override void Init()
    {

    }

    /// <summary>
    /// 设置游戏架构
    /// </summary>
    /// <param name="architecture"></param>
    public void SetGameArchitecture(IArchitecture architecture)
    {
        gameArchitecture = architecture;
    }

    /// <summary>
    /// 获取游戏架构
    /// </summary>
    /// <returns></returns>
    public IArchitecture GetGameArchitecture()
    {
        return gameArchitecture;
    }
    /// <summary>
    /// 初始化框架中的所有模块
    /// </summary>
    public void InitAllModulesInArchitecture()
    {
        gameArchitecture.InitAllModules();
    }

}