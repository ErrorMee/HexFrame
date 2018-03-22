using UnityEngine;
using System;
using UnityEngine.UI;

public class TipsView : ViewBase
{
    [SerializeField]
    private GameObject tipBar;

    private void Awake()
    {
        tipBar.gameObject.SetActive(false);
    }

    private void Start()
    {
        GameEvent.RegisterEvent(GameEventType.PopupTip, OnPopupTip);
    }

    private void OnPopupTip(object[] param)
    {
        string tipstr = (string)param[0];

        TipBar[] oldPopItems = transform.GetComponentsInChildren<TipBar>(false);
        for (int i = 0; i < oldPopItems.Length; i++)
        {
            TipBar oldPopItem = oldPopItems[i];
            oldPopItem.MoveUp();
        }

        GameObject popItem = GameObject.Instantiate(tipBar);
        popItem.gameObject.SetActive(true);
        popItem.transform.SetParent(transform, false);
        popItem.transform.localPosition = new Vector3();
        TipBar popItemCtr = popItem.GetComponent<TipBar>();
        popItemCtr.SetText(tipstr);
    }
}
