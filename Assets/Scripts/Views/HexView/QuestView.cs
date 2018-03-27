using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestView : ViewBase
{
    public Transform pauseBtn;

    private void Awake()
    {
        EventTriggerListener.Get(pauseBtn.gameObject).onClick = OnClickPauseBtn;
    }

    private void OnClickPauseBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("PauseView");
    }

}
