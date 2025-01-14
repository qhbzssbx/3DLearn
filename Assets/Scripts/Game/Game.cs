using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class Game : Architecture<Game>
{
    protected override void Init()
    {
        RegisterSystem(new AssetSystem());
        RegisterSystem(new UISystem());
        RegisterSystem(new SceneSystem());

        RegisterModel(new PlayerModel());
    }
}
