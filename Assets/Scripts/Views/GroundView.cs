using UnityEngine;
using System;
using UnityEngine.UI;

public class GroundView : ViewBase
{ 
    [SerializeField]
    private Image prefab;
    [SerializeField]
    private Transform cellList;

    private int otherness = 3;

    private bool firstCreateNode = true;

    private static HexGrid grid;

    private void Awake()
    {
        grid = HexGridModel.Instance.CreateEmptyGrid(11, 13);
        grid.TraversalNodes(CreateNode);
    }

    public static void SetGridPos(Transform tf,int posx,int posy)
    {
        HexNode node = grid.nodes[posy][posx];
        tf.localPosition = node.GetViewCoord();
    }

    private void CreateNode(HexNode node)
    {
        if (node.ArrayCoord.x <= 0 || node.ArrayCoord.x >= 10)
        {
            return;
        }

        Image cell;
        if (firstCreateNode)
        {
            firstCreateNode = false;
            cell = prefab;
        } else
        {
            cell = Instantiate<Image>(prefab);
        }

        cell.transform.SetParent(cellList, false);
        
        cell.transform.localPosition = node.GetViewCoord();

        int v = (int)(node.ArrayCoord.y) % otherness;//0 1 2

        if ((int)(node.ArrayCoord.x) % 2 != 0)
        {
            v--;
            if (v < 0)
            {
                v = otherness - 1;
            }
        }

        cell.color = new Color(cell.color.r + 0.001f * v, cell.color.g + 0.001f * v, cell.color.b + 0.001f * v, cell.color.a + 0.02f * v);

        //cell.isStatic = true;
    }
}