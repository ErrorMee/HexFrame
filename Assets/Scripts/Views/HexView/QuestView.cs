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
    }

    private void OnClickHomeBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("HomeView");
    }

}
