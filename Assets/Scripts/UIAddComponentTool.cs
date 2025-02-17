using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class UIAddComponentTool : MonoBehaviour
{

    string savePath = "Assets/Scripts/GameFrame/UI";

    [Button("AddBindScript")]    
    public void OnScriptReload()
    {
        string filePath = Path.Combine(savePath, $"{gameObject.name}.Binder.cs");
        MonoScript monoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(filePath);
        // System.Type scriptType = Type.GetType(gameObject.name);
        System.Type scriptType = monoScript.GetClass();
        if (scriptType != null)
        {
            gameObject.AddComponent(scriptType);
            DestroyImmediate(this);
            
        }
    	Debug.Log("脚本编译完毕");
    }
}
