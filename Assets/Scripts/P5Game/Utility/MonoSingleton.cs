// using UnityEngine;

// /// <summary>
// /// 单例模式通用基类
// /// </summary>
// public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
// {

//     #region Singleton core

//     /// <summary>
//     /// 单例存储属性
//     /// </summary>
//     protected static T _Instance = null;

//     /// <summary>
//     /// 单例赋值
//     /// </summary>
//     protected virtual void Awake()
//     {
//         if (_Instance != null)
//         {
//             LogUtility.Log("单例预制体重复！控制脚本：" + typeof(T).ToString(), Color.red);
//             return;
//         }

//         _Instance = this as T;
//     }

//     #endregion

//     #region Get instance

//     /// <summary>
//     /// 获得单一实例，没有会自动在Helper下创建
//     /// </summary>
//     /// <value>单一实例</value>
//     public static T One()
//     {
//         if (_Instance == null)
//         {
//             GameObject container = new GameObject(typeof(T).Name);
//             _Instance = container.AddComponent<T>();

//             GameObject helper = GameObject.Find("Helper");

//             if (helper == null)
//             {
//                 helper = new GameObject("Helper");
//                 DontDestroyOnLoad(helper);
//             }

//             container.transform.SetParent(helper.transform);
//             container.transform.localPosition = Vector3.zero;
//             container.transform.localRotation = Quaternion.identity;
//             container.transform.localScale = Vector3.one;
//         }

//         return _Instance;
//     }

//     /// <summary>
//     /// 获得预制体的单例模式
//     /// </summary>
//     /// <param name="prefabPath">预制体路径</param>
//     /// <param name="parent">预制体父级元素</param>
//     /// <param name="isUIPanel">是否为UI预制体</param>
//     public static T One(string prefabPath, Transform parent, bool isUIPanel = false)
//     {
//         if (_Instance == null)
//         {
//             //Prefabs.InstantiateIdentity(
//             //    Assets.One().Load(prefabPath),
//             //    prefabPath.Substring(prefabPath.LastIndexOf("/") + 1),
//             //    parent,
//             //    isUIPanel
//             //);
//         }

//         return _Instance;
//     }

//     #endregion

//     #region Destroy instance

//     /// <summary>
//     /// 销毁单例
//     /// </summary>
//     public virtual void Discard()
//     {
//         Destroy(gameObject);
//         _Instance = null;
//     }

//     #endregion

//     #region 重写方法

//     protected virtual void Start()
//     {

//     }

//     #endregion
// }
