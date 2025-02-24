using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public static class AppInfo
{
    public static int AppVersion = 1;
}

public class GameModel : AbstractModel
{
    public int AppVersion {get => AppInfo.AppVersion; }
    private int resVersion = 0;
    public int ResVersion
    {
        get
        {
            if (resVersion == 0)
            {
                resVersion = PlayerPrefs.GetInt("ResVersion", 1);
            }
            return resVersion;
        }
        set
        {
            resVersion = value;
            PlayerPrefs.SetInt("ResVersion", value);
        }
    }

    protected override void OnInit()
    {
    }
}
