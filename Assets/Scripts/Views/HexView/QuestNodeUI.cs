using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class QuestNodeUI : MonoBehaviour
{

    public HexQuest data;
    
    public Text tip;

    public Toggle toggle;

    public Action<HexQuest> onSelect;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(OnSelectHander);
    }

    private void OnSelectHander(bool select)
    {
        if (select)
        {
            if (onSelect != null)
            {
                onSelect(data);
            }
        }
    }

    public void InitData(HexQuest _data)
    {
        data = _data;
        ShowTip(data.id.ToString());
    }

    public void ShowTip(string tips)
    {
        if (tip)
        {
            tip.text = tips;
        }
    }

}

