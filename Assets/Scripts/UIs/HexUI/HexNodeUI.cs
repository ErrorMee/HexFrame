using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexNodeUI : MonoBehaviour {

    public HexNode data;

    public Image icon;
    public Text tip;

    protected virtual void Awake()
    {
        ShowTip("");
    }

    private void OnEnable()
    {
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

    public virtual void InitData(HexNode data)
    {
        this.data = data;
        UpdatePos();
    }

    public void UpdatePos()
    {
        Vector3 position = data.GetViewCoord();
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

    public virtual void UpdateIcon()
    {
        
    }
}
