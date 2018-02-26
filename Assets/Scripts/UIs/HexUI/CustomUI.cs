using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUI : UIBase
{
    public HexNodeUI prefab;
    private List<HexNodeUI> cells = new List<HexNodeUI>();
    private HexGrid grid = new HexGrid();

    void Start () {

        grid.widthMax = 5;
        grid.heightMax = 5;
        grid.OriginCenter();

        int id = 0;
        for (int y = 0; y < grid.heightMax; y++)
        {
            List<HexNode> row = new List<HexNode>();
            grid.nodes.Add(row);
            for (int x = 0; x < grid.widthMax; x++)
            {
                HexNode node = new HexNode();
                node.originPoint = grid.originPoint;
                node.ArrayCoord = new Vector2(x, y);
                node.id = ++id;
                row.Add(node);
            }
        }
        
        grid.TraversalNodes(CreateCell);
    }

    private void CreateCell(HexNode node)
    {
        HexNodeUI cell = Instantiate<HexNodeUI>(prefab);
        cells.Add(cell);
        cell.transform.SetParent(transform, false);

        cell.InitData(node);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(0, 960), new Vector3(1080, 960));
        Gizmos.DrawLine(new Vector3(540, 0), new Vector3(540, 1920));
    }

}
