using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomModel : Singleton<CustomModel>
{
    public const int MAX_CUSTOM_QUEST_NUM = 99;

    private static int MAX_QUEST_ID;

    public List<HexQuest> questList;

    public HexQuest crtQuest;
    public int crtQuestIndex;

    public HexNodeMarker selectMarker;
    public HexNode selectNode;

    public Action<HexNode> updateNodeEvent;

    public void Init()
    {
        questList = new List<HexQuest>();
        
        FindAllFiles.ListFiles(PathUtil.GetQuestDir(HexQuestType.CUSTOM), false, LoadQuestFormFile);

        if (questList.Count < 1)
        {
            CreateQuest();
            crtQuest.Save();
        }
        else
        {
            if (crtQuestIndex <= 0 || crtQuestIndex > questList.Count)
            {
                crtQuestIndex = questList.Count;
            }
            crtQuest = questList[crtQuestIndex - 1];
        }
    }

    public HexQuest CreateQuest()
    {
        HexQuest quest = new HexQuest();
        quest.id = ++CustomModel.MAX_QUEST_ID;
        quest.type = HexQuestType.CUSTOM;
        questList.Add(quest);
        quest.grid = HexGridModel.Instance.CreateEmptyGrid();
        crtQuest = quest;
        return quest;
    }

    private void LoadQuestFormFile(string fileName)
    {
        HexQuest quest = new HexQuest();
        quest.type = HexQuestType.CUSTOM;
        quest.Load(fileName);
        questList.Add(quest);
        crtQuestIndex = questList.Count;
        CustomModel.MAX_QUEST_ID = Mathf.Max(CustomModel.MAX_QUEST_ID, quest.id);
    }

    public void SelectMarker(HexNodeMarker marker)
    {
        if (selectMarker == marker)
        {
            return;
        }
        selectMarker = marker;
        SetMarker(selectNode, selectMarker);
    }

    public void SelectNode(HexNode node)
    {
        if (selectNode == node)
        {
            return;
        }
        selectNode = node;
        SetMarker(selectNode, selectMarker);
    }

    private void SetMarker(HexNode node, HexNodeMarker marker)
    {
        if (node != null && marker != null)
        {
            node.marker = marker;
            if (updateNodeEvent != null)
            {
                updateNodeEvent(node);
            }
        }
    }
}
