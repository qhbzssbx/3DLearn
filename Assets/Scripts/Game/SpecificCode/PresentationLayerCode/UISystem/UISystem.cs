using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UIϵͳ
/// </summary>
public class UISystem : IUISystem
{
    private Dictionary<string, IBasePanel> panelDict = new Dictionary<string, IBasePanel>();
    public void Init()
    {
    }

    private void AddPanelToDict<T>(IBasePanel uiPanel) where T:IBasePanel
    {
        panelDict.Add(typeof(T).ToString(), uiPanel);
    }

}
