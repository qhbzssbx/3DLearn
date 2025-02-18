using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GenUIBindCode : Editor
{
    [MenuItem("Asset/生成并添加到Prefab")]
    public static void Gen()
    {
        string savePath = "Assets/Scripts/GameFrame/UI";


        GameObject go = Selection.activeGameObject;

        string filePath = Path.Combine(savePath, $"{go.name}.Binder.cs");

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("#if UNITY_EDITOR");
        sb.AppendLine("using Sirenix.OdinInspector;");
        sb.AppendLine("#endif");
        sb.AppendLine();
        sb.AppendLine("// 自动生成的绑定脚本");
        sb.AppendLine("namespace P5Game.UI");
        sb.AppendLine("{");
        sb.AppendLine($"\tpublic partial class {go.name} : MonoBehaviour");
        sb.AppendLine("\t{");
        sb.AppendLine("\t\tprivate GameObject go;");
        sb.AppendLine("\t\t[Button(\"AutoBind\")]");
        sb.AppendLine("\t\tpublic void AutoBind()");
        sb.AppendLine("\t\t{");
        sb.AppendLine("\t\t\tgo = gameObject;");
        sb.AppendLine("\t\t}");
        sb.AppendLine("\t}");
        sb.AppendLine("}");

        // 将生成的代码写入文件（例如放在 Assets/Generated 目录下）

        File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

        go.AddComponent<UIAddComponentTool>();

        AssetDatabase.Refresh();

        // Type type = Type.GetType(go.name);
        // go.AddComponent(type);
        // 由于脚本是新创建的，Unity 会在刷新后重新编译，此时可能无法立即获取到脚本类型
        // 可通过延迟调用的方式等待编译完成后再添加组件
        // EditorApplication.delayCall += () =>
        // {
        //     // 尝试通过完整类型名称获取新脚本的类型（默认程序集一般为 Assembly-CSharp）
        //     System.Type scriptType = Type.GetType(go.name);
        //     if (scriptType != null)
        //     {
        //         // 添加组件到选中的 GameObject 上
        //         // selectedObj.AddComponent(scriptType);
        //         go.AddComponent(scriptType);
        //         Debug.Log($"脚本 {go.name} 已成功添加到 {go.name} 上！");
        //     }
        //     else
        //     {
        //         Debug.LogWarning("未找到脚本类型，请等待 Unity 编译完成后手动添加。");
        //     }
        // };

    }
}
