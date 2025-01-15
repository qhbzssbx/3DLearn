using System.Collections;
using System.Collections.Generic;
using GF;
using UnityEngine;

public class GameArchitecture : Architecture<GameArchitecture>
{
    protected override void Init()
    {
        this.RegistSystem<ISystem>(new AssetSystem());
        this.RegistSystem<IUISystem>(new UISystem());
    }
}

