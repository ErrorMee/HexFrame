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
        if (questList != null)
        {
            return;
        }
        questList = new List<HexQuest>();
        
        FindAllFiles.ListFiles(PathUtil.GetQuestDir(HexQuestType.CUSTOM), false, LoadQuestFormFile);

        if (questList.Count < 1)
        {
            CreateQuest();
            AttemptSave();
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

    public void UpdateQuestIndex(HexQuest quest)
    {
        CustomModel.Instance.crtQuestIndex = CustomModel.Instance.GetQuestIndex(quest);
        UpdateCrtQuest();
    }

    private int GetQuestIndex(HexQuest quest)
    {
        for (int i = 0;i< questList.Count;i++)
        {
            if (questList[i] == quest)
            {
                return i + 1;
            }
        }
        return -1;
    }

    private void UpdateCrtQuest()
    {
        crtQuest = questList[crtQuestIndex - 1];
    }

    public HexQuest CreateQuest()
    {
        HexQuest quest = new HexQuest();
        quest.id = ++CustomModel.MAX_QUEST_ID;
        quest.type = HexQuestType.CUSTOM;
        quest.needSave = true;
        questList.Add(quest);
        crtQuestIndex = questList.Count;
        quest.grid = HexGridModel.Instance.CreateEmptyGrid(HexGridModel.WIDTH, HexGridModel.HEIGHT);
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
        //SetMarker(selectMarker);
    }

    public void SelectNode(HexNode node)
    {
        if (selectNode == node)
        {
            return;
        }
        selectNode = node;
        SetMarker(selectMarker);
    }

    private void SetMarker(HexNodeMarker marker)
    {
        if (selectNode != null && marker != null)
        {
            int routes = crtQuest.grid.QueryRoute(selectNode, marker);
            if (routes < 1)
            {
                GameEvent.PopTip(ColorUtil.ColorString(Color.red, "×"));
            }
            else
            {
                selectNode.marker = marker;
                if (updateNodeEvent != null)
                {
                    crtQuest.needSave = true;
                    updateNodeEvent(selectNode);
                }
            }
        }
    }

    public void AttemptSave()
    {
        if (crtQuest.needSave)
        {
            crtQuest.needSave = false;
            crtQuest.Save();
        }
    }
}
