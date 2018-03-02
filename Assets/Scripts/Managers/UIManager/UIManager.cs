﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBehaviour<UIManager>
{
    private Vector3 lastTouchPos;

    private Transform uiRootT;

    //打开中的UI
    Dictionary<string,UIBase> m_openedDic = new Dictionary<string, UIBase>();
    //可恢复的UI
    List<UIContext> m_recoverList = new List<UIContext>();

    private void Awake()
    {
        uiRootT = Game.Instance.canvasTrans.Find("UIRoot").transform;
    }

    public void OpenUI(string name)
    {
        UIBase ui;
        if (m_openedDic.TryGetValue(name,out ui))
        {
            OnOpenUI(ui);
            return;
        }
        
        ResourceManager.Instance.GetGameObject(name, (obj) =>
        {
            ui = obj.GetComponent<UIBase>();
            ui.uiContext.uiName = name;
            //UI内存路线：实例 Dictionary 引用 1
            m_openedDic[name] = ui;
            AttachUI(ui);
            OnOpenUI(ui);
        });
    }

    protected void AttachUI(UIBase ui)
    {
        //UI内存路线：实例 显示列表 引用 2
        ui.transform.SetParent(uiRootT, false);
    }

    private void OnOpenUI(UIBase ui)
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
                HashSet<UIBase> removeList = new HashSet<UIBase>();
                foreach (UIBase uiBase in m_openedDic.Values)
                {
                    if (uiBase.uiContext.CanRecover() && ui.uiContext.uiName != uiBase.uiContext.uiName)
                    {
                        removeList.Add(uiBase);
                    }
                }

                foreach (UIBase item in removeList)
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

    public bool CloseUI(string name)
    {
        UIBase ui;
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

    private void OnCloseUI(UIBase ui)
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
