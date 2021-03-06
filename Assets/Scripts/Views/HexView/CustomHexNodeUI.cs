﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CustomHexNodeUI : HexNodeUI
{
    public Toggle toggle;

    protected override void Awake()
    {
        base.Awake();
		DOTween.To(() => icon.color, x => icon.color = x, new Color(64 / 255f, 64 / 255f, 64 / 255f, 1), 0.2f);
    }

    protected override void ClickNode(GameObject go)
    {
        base.ClickNode(go);

        CustomModel.Instance.SelectNode(data);
    }

    public override void UpdateIcon()
    {
        base.UpdateIcon();

        if (data.marker.nodeType != HexNodeType.NONE)
        {
            DOTween.To(() => icon.color, x => icon.color = x, new Color(240 / 255f, 240 / 255f, 240 / 255f, 1), 0.2f);
            ShowTip(data.marker.nodeType.ToString());
        }
        else
        {
			DOTween.To(() => icon.color, x => icon.color = x, new Color(64 / 255f, 64 / 255f, 64 / 255f, 1), 0.2f);
            ShowTip(string.Empty);
        }
    }
}
