using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CustomHexNodeUI : HexNodeUI
{

    protected override void Awake()
    {
        base.Awake();
        DOTween.To(() => icon.color, x => icon.color = x, new Color(32 / 255f, 32 / 255f, 32 / 255f, 1), 0.2f);
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
            DOTween.To(() => icon.color, x => icon.color = x, new Color(248 / 255f, 248 / 255f, 248 / 255f, 1), 0.2f);
            ShowTip(data.marker.nodeType.ToString());
        }
        else
        {
            DOTween.To(() => icon.color, x => icon.color = x, new Color(32 / 255f, 32 / 255f, 32 / 255f, 1), 0.2f);
            ShowTip(string.Empty);
        }
    }
}
