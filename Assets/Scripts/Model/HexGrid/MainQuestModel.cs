using System;
using System.Collections.Generic;

public class MainQuestModel : Singleton<MainQuestModel>
{
    public List<HexQuest> questList;
    public HexQuest crtQuest;

    public void Init()
    {
        if (questList != null)
        {
            return;
        }
        questList = new List<HexQuest>();

        FindAllFiles.ListFiles(PathUtil.GetQuestDir(HexQuestType.MAIN), false, LoadQuestFormFile);
    }

    private void LoadQuestFormFile(string fileName)
    {
        HexQuest quest = new HexQuest();
        quest.type = HexQuestType.MAIN;
        quest.Load(fileName);
        questList.Add(quest);
    }
}