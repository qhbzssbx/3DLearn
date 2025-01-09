using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIÃæ°å»ùÀà
/// </summary>
public class BasePanel : MonoBehaviour, IBasePanel
{
    protected IUISystem uiSystem;

    public virtual void OnClose()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnInit()
    {
        uiSystem = this.GetSystem<UISystem>();
        gameObject.SetActive(false);
    }

    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }
    // Start is called before the first frame update

}
