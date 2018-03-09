using UnityEngine.UI;
using UnityEngine;

public class HexNodeMarkerUI : MonoBehaviour {

    public HexNodeMarker data;

    public Image icon;
    public Text tip;


    private void OnEnable()
    {
        EventTriggerListener.Get(gameObject).onEnter = EnterNode;
        EventTriggerListener.Get(gameObject).onExit = ExitNode;
        EventTriggerListener.Get(gameObject).onClick = ClickNode;
    }

    private void EnterNode(GameObject go)
    {

    }

    private void ExitNode(GameObject go)
    {

    }

    private void ClickNode(GameObject go)
    {
        CustomModel.Instance.SelectMarker(data);
    }

    public void InitData(HexNodeMarker _data)
    {
        data = _data;
        ShowTip(data.nodeType.ToString());
    }

    public void ShowTip(string tips)
    {
        if (tip)
        {
            tip.text = tips;
        }
    }
    
}

