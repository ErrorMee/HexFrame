using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class QuestView : ViewBase
{
    public Transform homeButton;

	public Transform nodeList;

	public HexNodeUI prefabNode;

	private List<HexNodeUI> cells = new List<HexNodeUI>();

    private void Awake()
    {
        EventTriggerListener.Get(homeButton.gameObject).onClick = OnClickHomeBtn;
        GroundView.SetGridPos(homeButton, 8, 12);

		CreateList();
    }

    private void OnClickHomeBtn(GameObject go)
    {
        UIManager.Instance.OpenUI("MainView");
    }

	private void CreateList()
	{
        MainQuestModel.Instance.crtQuest.grid.TraversalNodes(CreateNode);
	}

	private void CreateNode(HexNode node)
	{
		HexNodeUI cell = Instantiate<HexNodeUI>(prefabNode);
		cells.Add(cell);
		cell.transform.SetParent(nodeList, false);

		cell.InitData(node);
	}

}
