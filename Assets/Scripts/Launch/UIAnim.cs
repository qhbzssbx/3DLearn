using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class UIAnim : MonoBehaviour
{
    public Transform root;
    public Transform root2;
    public Transform transform_1;
    public Transform transform_2;
    public Transform transform_3;

    private bool animEnd = false;
    // Start is called before the first frame update
    void Start()
    {
        root.DORotate(new Vector3(0, 0, 360), 5, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        root2.DORotate(new Vector3(0, 0, -360), 5, RotateMode.FastBeyond360).SetEase(Ease.Linear).OnComplete(()=>{
            CheckInitStatue();
            GameArchitecture.Interface.SendCommand<SetLaunchStageToAnimEndCommand>(new SetLaunchStageToAnimEndCommand());
        });
        transform_1.DORotate(new Vector3(0, 0, 360), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(5);
        transform_2.DORotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckInitStatue()
    {
        transform_3.DORotate(new Vector3(0, 0, -360), 5, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    private void OnDestroy() {
        root.DOKill();
        root2.DOKill();
        transform_1.DOKill();
        transform_2.DOKill();
        transform_3.DOKill();
    }

}
