using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    private Vector3 lastTouchPos;

    private Transform uiRootT;

    /// <summary>
    /// 打开中的UI
    /// </summary>
    Dictionary<string,ViewBase> m_openedDic = new Dictionary<string, ViewBase>();

    /// <summary>
    /// 可恢复的UI
    /// </summary>
    List<UIContext> m_recoverList = new List<UIContext>();

    /// <summary>
    /// UI分层
    /// </summary>
    List<Transform> uiLayers = new List<Transform>();

    private void Awake()
    {
        uiRootT = Game.Instance.canvasTrans.Find("UIRoot").transform;

        string[] uiLayerNames = Enum.GetNames(typeof(UIType));
        for (int i = 0; i < uiLayerNames.Length; i++)
        {
            string uiLayerName = uiLayerNames[i];
            GameObject uiLayer = new GameObject();
            RectTransform rectTranform = uiLayer.AddComponent<RectTransform>();
            rectTranform.anchorMin = new Vector2();
            rectTranform.anchorMax = new Vector2(1,1);
            rectTranform.sizeDelta = new Vector2();
            GLog.Log(rectTranform.rect.ToString());
            uiLayer.transform.SetParent(uiRootT, false);
            uiLayer.name = uiLayerName;
            uiLayers.Add(uiLayer.transform);
        }
    }

    public void OpenUI(string name)
    {
        ViewBase ui;
        if (m_openedDic.TryGetValue(name,out ui))
        {
            OnOpenUI(ui);
            return;
        }
        
        ResourceManager.Instance.GetGameObject(name, (obj) =>
        {
            ui = obj.GetComponent<ViewBase>();
            ui.uiContext.uiName = name;
            //UI内存路线：实例 Dictionary 引用 1
            m_openedDic[name] = ui;
            AttachUI(ui);
            OnOpenUI(ui);
        });
    }

    protected void AttachUI(ViewBase ui)
    {
        //UI内存路线：实例 显示列表 引用 2
        Transform uiLayer = uiLayers[(int)ui.uiContext.uiType];
        ui.transform.SetParent(uiLayer, false);
    }
    
    /// <summary>
    /// 打开的如果是全屏界面 关闭其他所有全屏和窗口
    /// </summary>
    /// <param name="ui"></param>
    private void OnOpenUI(ViewBase ui)
    {
        ui.gameObject.SetActive(true);
        if (ui.uiContext.CanRecover())
        {
            bool isNew = true;
            for (int i = 0; i < m_recoverList.Count; i++)
            {
                UIContext uiContext = m_recoverList[i];
                if (uiContext.uiName == ui.uiContext.uiName)
                {
                    isNew = false;
                    break;
                }
            }

            if (ui.uiContext.uiType == UIType.FULL)
            {
                HashSet<ViewBase> removeList = new HashSet<ViewBase>();
                foreach (ViewBase uiBase in m_openedDic.Values)
                {
                    if (uiBase.uiContext.CanRecover() && ui.uiContext.uiName != uiBase.uiContext.uiName)
                    {
                        removeList.Add(uiBase);
                    }
                }

                foreach (ViewBase item in removeList)
                {
                    OnCloseUI(item);
                }
            }

            if (isNew)
            {
                m_recoverList.Insert(0,ui.uiContext.Clone());
            }
        }
    }

    /// <summary>
    /// 关闭的如果是全屏的话 恢复前一个全屏和窗口
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CloseUI(string name)
    {
        ViewBase ui;
        if (m_openedDic.TryGetValue(name, out ui))
        {
            OnCloseUI(ui);

            for (int i = 0; i < m_recoverList.Count; i++)
            {
                UIContext uiContext = m_recoverList[i];
                if (uiContext.uiName == ui.uiContext.uiName)
                {
                    m_recoverList.Remove(uiContext);
                    break;
                }
            }

            if (ui.uiContext.uiType == UIType.FULL)
            {
                List<UIContext> openList = new List<UIContext>();
                for (int i = 0; i < m_recoverList.Count; i++)
                {
                    UIContext uiContext = m_recoverList[i];
                    if (uiContext.CanRecover() && uiContext.uiName != ui.uiContext.uiName)
                    {
                        openList.Insert(0, uiContext);
                        if (uiContext.uiType == UIType.FULL)
                        {
                            break;
                        }
                    }
                }
                for (int i = 0; i < openList.Count; i++)
                {
                    OpenUI(openList[i].uiName);
                }
            }
            
            return true;
        }
        return false;
    }

    private void OnCloseUI(ViewBase ui)
    {
        m_openedDic.Remove(ui.uiContext.uiName);
        ResourceManager.Instance.DestroyGameObj(ui.gameObject);
    }

    private void Update()
    {
        //lastTouchPos = Input.mousePosition;
        //GLog.Log("lastTouchPos " + lastTouchPos.x + "," + lastTouchPos.y + "," + lastTouchPos.z);
    }
}
