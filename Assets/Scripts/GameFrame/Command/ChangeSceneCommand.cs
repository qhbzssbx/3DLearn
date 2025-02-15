using QFramework;
using UnityEngine.SceneManagement;

public class ChangeSceneCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        AssetManager.Instance.ChangeScene();
    }
}
