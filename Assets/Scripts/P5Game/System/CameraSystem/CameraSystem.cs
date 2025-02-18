using System.Collections;
using System.Collections.Generic;
using P5Game;
using QFramework;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraSystem : AbstractSystem
{

    public Camera MainCamera { get; private set; }
    public Camera UICamera { get; private set; }

    protected override void OnInit()
    {
        UpdateCarmera();
    }

    public void UpdateCarmera()
    {
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").gameObject.GetComponent<Camera>();
        UICamera = GameObject.FindGameObjectWithTag("UICamera").gameObject.GetComponent<Camera>();

        AddCameraToMainCameraStack(UICamera);
    }

    public void AddCameraToMainCameraStack(Camera camera)
    {
        var cameraData = MainCamera.GetUniversalAdditionalCameraData();
        if (!cameraData.cameraStack.Contains(camera))
            cameraData.cameraStack.Add(camera);
        else
            LogUtility.LogWarning("添加摄像机失败, 已存在");
    }
}
