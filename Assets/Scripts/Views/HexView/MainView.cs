
using UnityEngine;

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

    private void InitMainQuest()
    {
        for (int i = 0; i < MainQuestModel.Instance.questList.Count; i++)
        {
            HexQuest quest = MainQuestModel.Instance.questList[i];
            CreateMainNode(quest);
        }
    }

    private void CreateMainNode(HexQuest quest)
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
                MainQuestModel.Instance.crtQuest = quest;
                UIManager.Instance.OpenUI("QuestView");
            };
    }

    private void OnClickCustomBtn(GameObject go)
    {
        CustomModel.Instance.crtQuest = null;
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
                CustomModel.Instance.crtQuest = quest;
                UIManager.Instance.OpenUI("CustomView");
            };
    }
}
