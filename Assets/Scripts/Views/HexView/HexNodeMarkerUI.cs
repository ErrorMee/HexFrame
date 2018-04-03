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
		switch(data.nodeType)
		{
			case HexNodeType.NONE:
                UIManager.Instance.SetImage(icon, "Marker00");
			    break;
            case HexNodeType.ORIGIN:
                UIManager.Instance.SetImage(icon, "Marker01");
                icon.color = Color.green;
                break;
            case HexNodeType.END:
                UIManager.Instance.SetImage(icon, "Marker01");
                icon.color = Color.yellow;
                break;
            case HexNodeType.NORM:
                UIManager.Instance.SetImage(icon, "Marker01");
                break;
            case HexNodeType.ORDER:
                UIManager.Instance.SetImage(icon, "Marker01");
                ShowTip(((int)data.order).ToString());
                break;
            case HexNodeType.FREQUENCY:
                UIManager.Instance.SetImage(icon, "Marker02");
                break;
            //case HexNodeType.BRIDGE:
            //    UIManager.Instance.SetImage(icon, "Marker01");
            //    break;
            default:
                UIManager.Instance.SetImage(icon, "Marker01");
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

