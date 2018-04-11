using UnityEngine.UI;
using UnityEngine;

public class HexNodeMarkerUI : MonoBehaviour {

    public HexNodeMarker data;

    public Image icon;
    public Text tip;

    private void Awake()
    {
        tip.text = "";
    }

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
        UIManager.Instance.SetImage(icon, data.icon);
        switch (data.nodeType)
		{
			case HexNodeType.NONE:
			    break;
            case HexNodeType.ORIGIN:
                icon.color = Color.green;
                break;
            case HexNodeType.END:
                icon.color = Color.yellow;
                break;
            case HexNodeType.NORM:
                icon.color = Color.gray;
                break;
            case HexNodeType.ORDER:
                ShowTip(((int)data.order).ToString());
                icon.color = Color.gray;
                break;
            case HexNodeType.FREQUENCY:
                icon.color = Color.gray;
                break;
            case HexNodeType.BRIDGE:
                icon.color = Color.gray;
                break;
            default:
                ShowTip(data.nodeType.ToString());
			    break;
		}
    }

    public void ShowTip(string tips)
    {
        if (tip)
        {
            tip.text = tips;
        }
    }
    
}

