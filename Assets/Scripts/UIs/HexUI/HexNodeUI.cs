using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexNodeUI : MonoBehaviour {

    public HexNode hexNodeData;

    public Image icon;
    public Text tip;

    protected virtual void Awake()
    {
        
    }

    private void OnEnable()
    {
        ShowTip("");
        EventTriggerListener.Get(gameObject).onEnter = EnterNode;
        EventTriggerListener.Get(gameObject).onExit = ExitNode;
        EventTriggerListener.Get(gameObject).onClick = ClickNode;
    }

    protected virtual void EnterNode(GameObject go)
    {
        
    }

    protected virtual void ExitNode(GameObject go)
    {
        
    }

    protected virtual void ClickNode(GameObject go)
    {

    }

    public void InitData(HexNode data)
    {
        hexNodeData = data;
        UpdatePos();
    }

    public void UpdatePos()
    {
        Vector3 position = hexNodeData.GetViewCoord();
        transform.localPosition = position;

        //ShowTip(hexNodeData.ArrayPosString());
        //ShowTip(hexNodeData.HexPosString());
    }

    public void ShowTip(string tips)
    {
        if (tip)
        {
            tip.text = tips;
        }
    }

}
