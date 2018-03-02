using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHexNodeUI : HexNodeUI
{

    protected override void Awake()
    {
        base.Awake();

        icon.color = new Color(1, 1, 1, 0);
    }

    protected override void ClickNode(GameObject go)
    {
        base.ClickNode(go);

        GLog.Log("ClickNode " + hexNodeData.ArrayPosString());
    }
}
