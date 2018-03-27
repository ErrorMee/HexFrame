using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FinishView : ViewBase
{
    public Transform homeBtn;
    public Transform againBtn;
    public Transform nextBtn;
    public Transform shareBtn;

    private void Awake()
    {
        EventTriggerListener.Get(homeBtn.gameObject).onClick = OnClickHomeBtn;
        EventTriggerListener.Get(againBtn.gameObject).onClick = OnClickAgainBtn;
        EventTriggerListener.Get(nextBtn.gameObject).onClick = OnClickNextBtn;
        EventTriggerListener.Get(shareBtn.gameObject).onClick = OnClickShareBtn;
    }

    private void OnClickHomeBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("MainView");
    }

    private void OnClickAgainBtn(GameObject go)
    {
        //UIManager.Instance.OpenUI("CustomView");
    }

    private void OnClickNextBtn(GameObject go)
    {
        //UIManager.Instance.OpenUI("CustomView");
    }

    private void OnClickShareBtn(GameObject go)
    {
        //UIManager.Instance.OpenUI("CustomView");
    }
}
