using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HexNodeUI : MonoBehaviour {

    public HexNode hexNodeData;

    public Text tip;

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
        ShowTip(hexNodeData.HexPosString());
    }

    public void ShowTip(string tips)
    {
        tip.text = tips;
    }

}
