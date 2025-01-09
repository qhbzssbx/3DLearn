using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBasePanel : IController
{
    public void OnInit();
    public void OnShow();
    public void OnClose();
}
