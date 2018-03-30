using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MainView : ViewBase
{
    public Transform customBtn;

    public QuestNodeUI customPrefab;

    public QuestNodeUI mainPrefab;

    private void Awake()
    {
        
        EventTriggerListener.Get(customBtn.gameObject).onClick = OnClickCustomBtn;

        MainQuestModel.Instance.Init();

        InitMainQuest();

        CustomModel.Instance.Init();

        InitCustomQuest();
    }

    private void Start()
    {
        GroundView.SetGridPos(customBtn, 8, 1);
    }

    private void OnClickCustomBtn(GameObject go)
    {
        CustomModel.Instance.crtQuestIndex = CustomModel.Instance.questList.Count + 1;
        UIManager.Instance.OpenUI("CustomView");
    }

    private void InitCustomQuest()
    {
        for (int i = 0; i < CustomModel.Instance.questList.Count; i++)
        {
            HexQuest quest = CustomModel.Instance.questList[i];
            CreateCustomNode(quest);
        }
    }

    private void CreateCustomNode(HexQuest quest)
    {
        QuestNodeUI nodeQuest = customPrefab;

        if (quest.id != 1)
        {
            nodeQuest = Instantiate<QuestNodeUI>(customPrefab);
            nodeQuest.transform.SetParent(customPrefab.transform.parent, false);
        }

        nodeQuest.InitData(quest);

        EventTriggerListener.Get(nodeQuest.gameObject).onClick = 
            (go) => {
                CustomModel.Instance.UpdateQuestIndex(quest);
                UIManager.Instance.OpenUI("CustomView");
            };
    }

    private void InitMainQuest()
    {
        for (int i = 0; i < MainQuestModel.Instance.questList.Count; i++)
        {
            HexQuest quest = MainQuestModel.Instance.questList[i];
            CreateMianNode(quest);
        }
    }

    private void CreateMianNode(HexQuest quest)
    {
        QuestNodeUI nodeQuest = mainPrefab;

        if (quest.id != 1)
        {
            nodeQuest = Instantiate<QuestNodeUI>(mainPrefab);
            nodeQuest.transform.SetParent(mainPrefab.transform.parent, false);
        }

        nodeQuest.InitData(quest);

        EventTriggerListener.Get(nodeQuest.gameObject).onClick =
            (go) => {
                UIManager.Instance.OpenUI("QuestView");
            };
    }
}
