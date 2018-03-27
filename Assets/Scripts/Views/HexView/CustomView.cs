using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CustomView : ViewBase
{
    public QuestNodeUI prefabQuest;
    public Toggle prefabQuestNew;
    public List<QuestNodeUI> quests = new List<QuestNodeUI>();
    public SliderUtil listSliderUtil;
    
    public CustomHexNodeUI prefabCustom;
    private List<CustomHexNodeUI> cells = new List<CustomHexNodeUI>();

    public HexNodeMarkerUI prefabMarker;

    public Transform newButton;
    public Transform nodeList;
    public Transform markerList;

    public Text tip;

    public Transform homeBtn;

    private void Awake()
    {
        EventTriggerListener.Get(homeBtn.gameObject).onClick = OnClickHomeBtn;

        prefabQuestNew.onValueChanged.AddListener(OnSelectNewHander);

        listSliderUtil.SetHorizontalSlid(ListHorizontalSliderHander);
        
        CustomModel.Instance.updateNodeEvent = OnNodeUpdate;

        EventTriggerListener.Get(newButton.gameObject).onClick = OnNewButtonClick;

        CustomModel.Instance.Init();
        InitQuestList();
    }

    private void OnClickHomeBtn(GameObject go)
    {
        CustomModel.Instance.AttemptSave();
        UIManager.Instance.OpenUI("MainView");
    }
    
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        CreateMarkerList();
        CreateCustomList();
        quests[CustomModel.Instance.crtQuestIndex - 1].toggle.isOn = true;
    }

    private void InitQuestList()
    {
        for (int i = 0; i < CustomModel.Instance.questList.Count; i++)
        {
            HexQuest quest = CustomModel.Instance.questList[i];
            CreateQuestNode(quest);
        }
    }

    private void CreateQuestNode(HexQuest quest)
    {
        QuestNodeUI nodeQuest = prefabQuest;

        if (quest.id != 1)
        {
            nodeQuest = Instantiate<QuestNodeUI>(prefabQuest);
            nodeQuest.transform.SetParent(prefabQuest.transform.parent, false);
        }

        nodeQuest.InitData(quest);
        nodeQuest.onSelect = OnSelectQuest;
        quests.Add(nodeQuest);
        prefabQuestNew.transform.SetAsLastSibling();
    }

    private void CreateMarkerList()
    {
        for (int i = 0;i<HexNodeMarker.MARKERS.Count;i++)
        {
            HexNodeMarker marker = HexNodeMarker.MARKERS[i];
            CreateNodeMarker(marker);
        }
    }

    private void CreateNodeMarker(HexNodeMarker marker)
    {
        HexNodeMarkerUI nodeMarker = prefabMarker;
        if (marker.nodeType > HexNodeType.NONE)
        {
            nodeMarker = Instantiate<HexNodeMarkerUI>(prefabMarker);
            nodeMarker.transform.SetParent(prefabMarker.transform.parent, false);
        }

        nodeMarker.InitData(marker);
    }

    private void CreateCustomList()
    {
        CustomModel.Instance.crtQuest.grid.TraversalNodes(CreateNode);
    }

    private void CreateNode(HexNode node)
    {
        CustomHexNodeUI cell = Instantiate<CustomHexNodeUI>(prefabCustom);
        cells.Add(cell);
        cell.transform.SetParent(nodeList, false);
        
        cell.InitData(node);
    }

    private void UpdateCustomList()
    {
        int index = 0;
        CustomModel.Instance.crtQuest.grid.TraversalNodes(
            node => {
                CustomHexNodeUI cell = cells[index];
                cell.toggle.isOn = false;
                cell.InitData(node);
                index++;
            });
    }

    private void OnNodeUpdate(HexNode node)
    {
        foreach(var cell in cells)
        {
            if (cell.data.id == node.id)
            {
                cell.UpdateIcon();
                HexQuest crtQuest = CustomModel.Instance.crtQuest;
                tip.text = " route: " + crtQuest.grid.routes.Count + " step: " + crtQuest.grid.allStep + " rating: " + crtQuest.grid.rating;
                return;
            }
        }
    }

    private void ListHorizontalSliderHander(int dir)
    {
        
        CustomModel.Instance.crtQuestIndex = CustomModel.Instance.crtQuestIndex + dir;

        if (CustomModel.Instance.crtQuestIndex > CustomModel.Instance.questList.Count)
        {
            prefabQuestNew.isOn = true;
        }
        else
        {
            if (CustomModel.Instance.crtQuestIndex < 1)
            {
                CustomModel.Instance.crtQuestIndex = 1;
            }
            quests[CustomModel.Instance.crtQuestIndex - 1].toggle.isOn = true;
        }

    }

    private void OnSelectNewHander(bool select)
    {
        if (select)
        {
            CustomModel.Instance.AttemptSave();
            CustomModel.Instance.crtQuestIndex = CustomModel.Instance.questList.Count + 1;
            newButton.gameObject.SetActive(true);
            nodeList.gameObject.SetActive(false);
            markerList.gameObject.SetActive(false);
        }
    }

    private void OnSelectQuest()
    {
        CustomModel.Instance.AttemptSave();
        CustomModel.Instance.UpdateCrtQuest();

        tip.text = "";
        UpdateCustomList();
        newButton.gameObject.SetActive(false);
        nodeList.gameObject.SetActive(true);
        markerList.gameObject.SetActive(true);
    }

    private void OnNewButtonClick(GameObject go)
    {
        CustomModel.Instance.CreateQuest();
        CreateQuestNode(CustomModel.Instance.crtQuest);
        quests[CustomModel.Instance.crtQuestIndex - 1].toggle.isOn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 960), new Vector3(1080, 960));
        Gizmos.DrawLine(new Vector3(540, 0), new Vector3(540, 1920));
    }

}
