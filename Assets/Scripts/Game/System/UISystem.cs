using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
public class UISystem : AbstractSystem
{
    private UIRoot uiRoot;
    protected override void OnInit()
    {
        uiRoot = UIRoot.Instance; // 触发UIRoot的加载
    }
}
