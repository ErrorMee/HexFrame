using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType:int
{
    /// <summary>
    /// 背景层
    /// </summary>
    BEAKDROP = 0,
    /// <summary>
    /// 全屏
    /// </summary>
    FULL,
    /// <summary>
    /// 窗口
    /// </summary>
    WINDOW,
    /// <summary>
    /// 上层
    /// </summary>
    TOP
}

[Serializable]
public class UIContext
{
    [HideInInspector]
    public string uiName;
    public UIType uiType = UIType.FULL;

    public UIContext Clone()
    {
        UIContext clone = new UIContext();
        clone.uiName = uiName;
        clone.uiType = uiType;
        return clone;
    }

    public bool CanRecover()
    {
        if (uiType == UIType.FULL || uiType == UIType.WINDOW)
        {
            return true;
        }
        return false;
    }
}

[DisallowMultipleComponent]
public class ViewBase : MonoBehaviour
{
    public UIContext uiContext;

    private void Awake()
    {
        
    }

    public virtual void OnOpen()
    {

    }

    protected void CloseSelf()
    {
        UIManager.Instance.CloseUI(uiContext.uiName);
    }
}
