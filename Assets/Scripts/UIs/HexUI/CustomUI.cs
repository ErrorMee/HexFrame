using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomUI : UIBase
{
    public HexNodeMarkerUI prefabMarker;
    public RectTransform hexNodeListRTF;
    public CustomHexNodeUI prefabCustom;
    private List<CustomHexNodeUI> cells = new List<CustomHexNodeUI>();

    private void Awake()
    {
        CustomModel.Instance.Init();
        CustomModel.Instance.updateNodeEvent = OnNodeUpdate;
    }

    void Start ()
    {
        CreateMarkerList();
        CreateCustomList();
    }

    private void CreateMarkerList()
    {
        for (int i = 0;i<HexGridModel.Instance.hexNodeMarkers.Count;i++)
        {
            HexNode node = HexGridModel.Instance.hexNodeMarkers[i];
            CreateNodeMarker(node);
        }
    }

    private void CreateNodeMarker(HexNode nodeDate)
    {
        HexNodeMarkerUI nodeMarker = prefabMarker;
        if (nodeDate.nodeType > HexNodeType.NONE)
        {
            nodeMarker = Instantiate<HexNodeMarkerUI>(prefabMarker);
            nodeMarker.transform.SetParent(prefabMarker.transform.parent, false);
        }

        nodeMarker.InitData(nodeDate);
    }

    private void CreateCustomList()
    {
        CustomModel.Instance.crtQuest.grid.TraversalNodes(CreateNode);
    }

    private void CreateNode(HexNode node)
    {
        CustomHexNodeUI cell = Instantiate<CustomHexNodeUI>(prefabCustom);
        cells.Add(cell);
        cell.transform.SetParent(hexNodeListRTF, false);
        
        cell.InitData(node);
    }

    private void OnNodeUpdate(HexNode node)
    {
        foreach(var cell in cells)
        {
            if (cell.hexNodeData.id == node.id)
            {
                cell.UpdateIcon();
                return;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 960), new Vector3(1080, 960));
        Gizmos.DrawLine(new Vector3(540, 0), new Vector3(540, 1920));
    }

}
