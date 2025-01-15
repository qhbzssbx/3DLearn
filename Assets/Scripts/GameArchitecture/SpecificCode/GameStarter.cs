using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏启动类
/// </summary>
public class GameStarter : MonoBehaviour, IController
{
    private StartArchitecture _startArchitecture;
    // Start is called before the first frame update
    void Start()
    {
        // 获取游戏架构, 懒汉式会直接实例化
        _startArchitecture = StartArchitecture.Instance;
        _startArchitecture.SetGameArchitecture(new GameArchitecture());
        _startArchitecture.InitAllModulesInArchitecture();

        this.SendCommand<OpenPanelCommand>("LoginPanel");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }
}
