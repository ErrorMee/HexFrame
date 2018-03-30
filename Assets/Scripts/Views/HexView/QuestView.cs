using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestView : ViewBase
{
    public Transform homeButton;

    private void Awake()
    {
        EventTriggerListener.Get(homeButton.gameObject).onClick = OnClickHomeBtn;
        GroundView.SetGridPos(homeButton, 8, 11);
    }

    private void OnClickHomeBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("MainView");
    }

}
