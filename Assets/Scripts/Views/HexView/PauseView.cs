using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PauseView : ViewBase
{
    public Transform continueBtn;

    public Transform homeBtn;

    private void Awake()
    {
        EventTriggerListener.Get(continueBtn.gameObject).onClick = OnClickContinueBtn;
        EventTriggerListener.Get(homeBtn.gameObject).onClick = OnClickHomeBtn;
    }

    private void OnClickContinueBtn(GameObject go)
    {
        CloseSelf();
    }

    private void OnClickHomeBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("MainView");
    }
}
