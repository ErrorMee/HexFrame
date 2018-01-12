using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FightUI : UIBase
{
    public Transform closeBtn;

    public Text title;

    public Text shareMenu;
    public Text shareView;
    public Text share;

    private void Start()
    {
        SDKManager.Instance.TestUnityCallAndorid();

        EventTriggerListener.Get(closeBtn.gameObject).onClick = OnClickClose;

        EventTriggerListener.Get(title.gameObject).onClick = OnClickTile;

        EventTriggerListener.Get(share.gameObject).onClick = OnClickShare;
        EventTriggerListener.Get(shareMenu.gameObject).onClick = OnClickShareMenu;
        EventTriggerListener.Get(shareView.gameObject).onClick = OnClickShareView;
    }

    private void OnClickTile(GameObject go)
    {
        SDKManager.Instance.TestAndoridCallUnity("FightUI_1", "TestAndoridCall");
    }

    private void OnClickClose(GameObject go)
    {
        CloseSelf();
    }

    public void TestAndoridCall(string str)
    {
        title.text = str;
    }

    private void OnClickShare(GameObject go)
    {
        SDKManager.Instance.TestShare();
    }

    private void OnClickShareMenu(GameObject go)
    {
        SDKManager.Instance.TestShareMenu();
    }

    private void OnClickShareView(GameObject go)
    {
        SDKManager.Instance.TestShareView();
    }
}
