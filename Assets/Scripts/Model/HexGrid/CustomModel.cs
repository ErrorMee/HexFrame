using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomModel : Singleton<CustomModel>
{


    public List<HexQuest> questList;

    public HexQuest crtQuest;

    public HexNodeMarker selectMarker;
    public HexNode selectNode;

    public Action<HexNode> updateNodeEvent;

    public void Init()
    {
        questList = new List<HexQuest>();

        FindAllFiles.ListFiles(PathUtil.GetQuestDir(HexQuestType.CUSTOM), false, LoadQuestFormFile);
        
        if (questList.Count < 1)
        {
            HexQuest quest = new HexQuest();
            quest.type = HexQuestType.CUSTOM;
            questList.Add(quest);
            quest.grid = HexGridModel.Instance.CreateEmptyGrid();
            crtQuest = quest;

            quest.Save();
        }
    }

    private void LoadQuestFormFile(string fileName)
    {
        HexQuest quest = new HexQuest();
        quest.type = HexQuestType.CUSTOM;
        quest.Load(fileName);
        questList.Add(quest);
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
