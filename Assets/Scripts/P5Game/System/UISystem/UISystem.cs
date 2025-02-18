using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;
using System.Reflection;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using YooAsset;
using UnityEngine.Rendering.Universal;

namespace P5Game.UI
{
    public interface IUIPanel : ICanGetModel
    {
        public void Show();
        public void Hide();
        public void Exit();
    }

    public abstract class UIPanel : MonoBehaviour, IUIPanel
    {
        public IAssetHandle handle;
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Exit()
        {
        }

        public virtual void OnShow()
        {
        }

        public virtual void OnExit()
        {
            Destroy(gameObject);
            if (handle != null)
            {
                handle.Release();
                handle = null;
            }
        }

        public IArchitecture GetArchitecture()
        {
            return GameArchitecture.Interface;
        }
    }

    public class UISystem : AbstractSystem
    {
        public Transform uiRoot;
        public Transform uiParent;

        private IUIResourceLoader uIResourceLoader;


        private static readonly Dictionary<string, Type> _panelTypes = new();
        private Stack<UIPanel> uIPanelStacks = new();


        private int sortingOrder = 100;
        private readonly int uiLayerInterval = 20;


        protected override void OnInit()
        {
            // 获取当前应用程序域中所有已加载的程序集
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var type in assemblies
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsSubclassOf(typeof(UIPanel))
                         && !t.IsAbstract
                         && !t.IsInterface))
            {
                _panelTypes[type.Name] = type;
                // 或用全名：type.FullName
            }

            uIResourceLoader = new DefaultUIResourceLoader();

            var assetHandle = uIResourceLoader.LoadPrefab("UI/UIRoot");
            uiRoot = assetHandle.GetGameObjectInstantiate().transform;
            assetHandle.Release();

            uiParent = uiRoot.Find("UICanvas").transform;

            GameObject.DontDestroyOnLoad(uiRoot);
        }

        public void OpenUI(string panelName)
        {
            if (_panelTypes.TryGetValue(panelName, out Type panelType))
            {
                IAssetHandle assetHandle = uIResourceLoader.LoadPrefab("UI/" + panelName);
                GameObject uiGo = assetHandle.GetGameObjectInstantiate();

                UIPanel panel = uiGo.GetComponent(panelType) as UIPanel;
                if (panel == null)
                {
                    panel = uiGo.AddComponent(panelType) as UIPanel;
                }
                panel.handle = assetHandle;
                

                var canvas = uiGo.GetComponent<Canvas>();
                if (canvas != null)
                {   
                    canvas.worldCamera = this.GetSystem<CameraSystem>().UICamera;
                    sortingOrder += uiLayerInterval;
                    canvas.sortingOrder = sortingOrder;
                }
                uiGo.transform.SetParent(uiParent);
                uiGo.transform.localPosition = Vector3.zero;
                uiGo.transform.localScale = new Vector3(1,1,1);

                uIPanelStacks.Push(panel);
                panel.Show();
            }
            else
            {
                throw new ArgumentException($"Panel {panelName} not found");
            }
        }


        public void OpenUI<T>() where T : UIPanel
        {
            OpenUI(typeof(T).Name);
        }

        /// <summary>
        /// 关闭顶层Panel
        /// </summary>
        public void CloseUI()
        {
            var panel = uIPanelStacks.Pop();
            panel.Exit();
            sortingOrder -= uiLayerInterval;
        }

        /// <summary>
        /// 关闭所有已打开的UIPanel
        /// </summary>
        public void CloseAllPanels()
        {
            while (uIPanelStacks.Count > 0)
            {
                UIPanel panel = uIPanelStacks.Pop();
                panel.Exit();
            }
        }

        /// <summary>
        /// 关闭指定的UIPanel，如果该面板存在于堆栈中
        /// </summary>
        /// <param name="panel">要关闭的UIPanel</param>
        public void ClosePanel(UIPanel panel)
        {
            if (panel == null)
                return;

            if (uIPanelStacks.Contains(panel))
            {
                // 为支持中间关闭，将堆栈中的面板全部出栈至临时栈，直到找到目标面板
                Stack<UIPanel> tempStack = new Stack<UIPanel>();
                bool found = false;
                while (uIPanelStacks.Count > 0)
                {
                    UIPanel temp = uIPanelStacks.Pop();
                    if (temp == panel)
                    {
                        found = true;
                        break;
                    }
                    else
                    {
                        tempStack.Push(temp);
                    }
                }
                // 将其他面板还原回原来的顺序
                while (tempStack.Count > 0)
                {
                    uIPanelStacks.Push(tempStack.Pop());
                }
                if (found)
                {
                    panel.Exit();
                }
            }
            else
            {
                Debug.LogWarning("The specified UIPanel is not managed by UIManager.");
            }
        }
    }


    public interface IUIResourceLoader
    {
        // /// <summary>
        // /// 加载指定类型的资源
        // /// </summary>
        // T Load<T>(string path) where T : UnityEngine.Object;

        /// <summary>
        /// 加载UI预制体
        /// </summary>
        IAssetHandle LoadPrefab(string path);
    }

    public interface IAssetHandle
    {
        GameObject GetGameObjectInstantiate();
        void Release();
    }

    public class YooAssetAssetHandle : IAssetHandle
    {
        private YooAsset.AssetHandle assetHandle;
        public GameObject Go { get; private set; }

        public YooAssetAssetHandle(YooAsset.AssetHandle assetHandle)
        {
            Go = assetHandle.AssetObject as GameObject;
        }

        public GameObject GetGameObjectInstantiate()
        {
            return GameObject.Instantiate(Go);
        }

        public void Release()
        {
            if (assetHandle != null)
            {
                assetHandle.Release();
            }
        }
    }

    public class DefaultAssetHandle : IAssetHandle
    {
        public GameObject Go { get; private set; }

        public DefaultAssetHandle(GameObject go)
        {
            Go = go;
        }

        public void Release()
        {
            // GameObject.Destroy(Go);
        }

        public GameObject GetGameObjectInstantiate()
        {
            return GameObject.Instantiate(Go);
        }
    }

    /// <summary>
    /// 默认实现，使用Unity内置的Resources加载方式。
    /// </summary>
    public class DefaultUIResourceLoader : IUIResourceLoader
    {
        public IAssetHandle LoadPrefab(string path)
        {
            return new DefaultAssetHandle(Resources.Load<GameObject>(path));
        }
    }
}
