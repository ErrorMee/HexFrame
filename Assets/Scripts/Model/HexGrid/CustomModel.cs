using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomModel : Singleton<CustomModel>
{

    public static int CUSTOM_WIDTH = 7;
    public static int CUSTOM_HEIGHT = 9;

    public List<CustomQuest> questList;

    public CustomQuest crtQuest;

    public HexNode selectType;
    public HexNode selectNode;

    public Action<HexNode> updateNodeEvent;

    public void Init()
    {
        questList = new List<CustomQuest>();

        if (questList.Count < 1)
        {
            CustomQuest quest = new CustomQuest();
            questList.Add(quest);
            quest.grid = HexGridModel.Instance.CreateEmptyGrid(CUSTOM_WIDTH, CUSTOM_HEIGHT);
            crtQuest = quest;
        }
    }

    public void SelectType(HexNode node)
    {
        if (selectType == node)
        {
            return;
        }
        selectType = node;
        if (selectNode != null)
        {
            selectNode.nodeType = selectType.nodeType;
            if (updateNodeEvent != null)
            {
                updateNodeEvent(selectNode);
            }
        }
    }

    public void SelectNode(HexNode node)
    {
        if (selectNode == node)
        {
            return;
        }
        selectNode = node;
        if (selectType != null)
        {
            selectNode.nodeType = selectType.nodeType;
            if (updateNodeEvent != null)
            {
                updateNodeEvent(selectNode);
            }
        }
    }
}
