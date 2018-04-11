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
    private List<Vector2> markerPoss = new List<Vector2> {new Vector2(2, 1), new Vector2(3, 1), new Vector2(4, 1),
     new Vector2(5,1), new Vector2(6,1), new Vector2(7,1), new Vector2(8,1), new Vector2(3,0), new Vector2(5,0), new Vector2(7,0)};

    public Transform newButton;
    public Transform nodeList;
    public Transform markerList;
	public Transform questList;

    public Text tip;

    public Transform homeBtn;

    private void Awake()
    {
        prefabQuest.gameObject.SetActive(false);
        GroundView.SetGridPos(homeBtn, 8, 12);
        GroundView.SetGridPos(newButton, 5, 6);
        EventTriggerListener.Get(homeBtn.gameObject).onClick = OnClickHomeBtn;

        prefabQuestNew.onValueChanged.AddListener(OnSelectNewHander);

        listSliderUtil.SetHorizontalSlid(ListHorizontalSliderHander);
        
        CustomModel.Instance.updateNodeEvent = OnNodeUpdate;

        EventTriggerListener.Get(newButton.gameObject).onClick = OnNewButtonClick;
        
        InitQuestList();
        CreateCustomList();
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
    }

    private void OnEnable()
    {
        ShowQuest();
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
        nodeQuest.gameObject.SetActive(true);
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
            CreateNodeMarker(marker,i);
        }
    }

    private void CreateNodeMarker(HexNodeMarker marker,int index)
    {
        HexNodeMarkerUI nodeMarker = prefabMarker;
        if (marker.nodeType > HexNodeType.NONE)
        {
            nodeMarker = Instantiate<HexNodeMarkerUI>(prefabMarker);
            nodeMarker.transform.SetParent(prefabMarker.transform.parent, false);
        }
        Vector2 pos = markerPoss[index];
        GroundView.SetGridPos(nodeMarker.transform, (int)pos.x, (int)pos.y);
        nodeMarker.InitData(marker);
    }

    private void CreateCustomList()
    {
        for (int column = 0; column < HexGridModel.HEIGHT; column++)
        {
            for (int row = 0; row < HexGridModel.WIDTH; row++)
            {
                CustomHexNodeUI cell = Instantiate<CustomHexNodeUI>(prefabCustom);
                cells.Add(cell);
                cell.transform.SetParent(nodeList, false);
            }
        }
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
        if (CustomModel.Instance.crtQuest == null)
        {
            if (dir < 0)
            {
                if (CustomModel.Instance.questList.Count > 0)
                {
                    CustomModel.Instance.crtQuest = CustomModel.Instance.questList[CustomModel.Instance.questList.Count - 1];
                }
            }
        } else
        {
            int crtIndex = CustomModel.Instance.questList.IndexOf(CustomModel.Instance.crtQuest);
            int toIndex = crtIndex + dir;
            if (toIndex >= 0)
            {
                if (toIndex >= CustomModel.Instance.questList.Count)
                {
                    CustomModel.Instance.crtQuest = null;
                }
                else
                {
                    CustomModel.Instance.crtQuest = CustomModel.Instance.questList[toIndex];
                }
            }
            else
            {
                CustomModel.Instance.crtQuest = null;
            }
        }

        ShowQuest();
    }

    private void ShowQuest()
    {
        prefabQuestNew.group.SetAllTogglesOff();
        StartCoroutine(DelayShowQuest());
    }

    IEnumerator DelayShowQuest()
    {
        if (CustomModel.Instance.crtQuest == null)
        {
            nodeList.gameObject.SetActive(false);
            markerList.gameObject.SetActive(false);
            yield return 0;
            prefabQuestNew.isOn = true;
        }
        else
        {
            prefabQuestNew.isOn = false;
            yield return 0;
            quests[CustomModel.Instance.questList.IndexOf(CustomModel.Instance.crtQuest)].toggle.isOn = true;
        }
    }

    private void OnSelectNewHander(bool select)
    {
        if (select)
        {
            CustomModel.Instance.AttemptSave();
            CustomModel.Instance.crtQuest = null;
            newButton.gameObject.SetActive(true);
            nodeList.gameObject.SetActive(false);
            markerList.gameObject.SetActive(false);
        }
    }

    private void OnSelectQuest(HexQuest quest)
    {
        CustomModel.Instance.AttemptSave();
        CustomModel.Instance.crtQuest = quest;

        tip.text = "";
        UpdateCustomList();
        newButton.gameObject.SetActive(false);
        nodeList.gameObject.SetActive(true);
        markerList.gameObject.SetActive(true);
		questList.gameObject.SetActive(true);
    }

    private void OnNewButtonClick(GameObject go)
    {
        CustomModel.Instance.crtQuest = CustomModel.Instance.CreateQuest();
        CreateQuestNode(CustomModel.Instance.crtQuest);
        quests[CustomModel.Instance.questList.Count - 1].toggle.isOn = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 960), new Vector3(1080, 960));
        Gizmos.DrawLine(new Vector3(540, 0), new Vector3(540, 1920));
    }

}
