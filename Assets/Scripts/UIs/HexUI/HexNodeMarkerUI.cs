using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNodeMarkerUI : HexNodeUI
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void ClickNode(GameObject go)
    {
        base.ClickNode(go);

        CustomModel.Instance.SelectType(hexNodeData);
    }

    public override void InitData(HexNode data)
    {
        hexNodeData = data;
        ShowTip(hexNodeData.nodeType.ToString());
    }
}
