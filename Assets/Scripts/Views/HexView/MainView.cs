using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainView : ViewBase
{
    public Transform customBtn;

    private void Awake()
    {
        EventTriggerListener.Get(customBtn.gameObject).onClick = OnClickCustomBtn;
    }

    private void OnClickCustomBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("CustomView");
    }

}
