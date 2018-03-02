using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CustomUI : UIBase
{
    public HexNodeTypeUI prefabType;
    public RectTransform hexNodeListRTF;
    public CustomHexNodeUI prefab;
    private List<HexNodeUI> cells = new List<HexNodeUI>();
    private HexGrid grid = new HexGrid();

    void Start ()
    {
        CreateTypeList();
        CreateCustomList();
    }

    private void CreateTypeList()
    {
        Array values = Enum.GetValues(typeof(HexNodeType));
        foreach (int value in values)
        {
            CreateNodeType(value);
        }
    }

    private void CreateNodeType(int value)
    {
        HexNodeTypeUI cellType = prefabType;
        if (value > (int)HexNodeType.NONE)
        {
            cellType = Instantiate<HexNodeTypeUI>(prefabType);
            cellType.transform.SetParent(prefabType.transform.parent, false);
        }
    }

    private void CreateCustomList()
    {
        grid = CustomModel.Instance.CreateGrid(7, 9);
        grid.TraversalNodes(CreateNode);
    }

    private void CreateNode(HexNode node)
    {
        CustomHexNodeUI cell = Instantiate<CustomHexNodeUI>(prefab);
        cells.Add(cell);
        cell.transform.SetParent(hexNodeListRTF, false);
        
        cell.InitData(node);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 960), new Vector3(1080, 960));
        Gizmos.DrawLine(new Vector3(540, 0), new Vector3(540, 1920));
    }

}
