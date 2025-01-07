using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏启动类
/// </summary>
public class GameStarter : MonoBehaviour
{
    private StartArchitecture startArchitecture;
    // Start is called before the first frame update
    void Start()
    {
        // 获取游戏架构, 懒汉式会直接实例化
        startArchitecture = StartArchitecture.Instance;
        startArchitecture.SetGameArchitecture(new WowArchitecture());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
