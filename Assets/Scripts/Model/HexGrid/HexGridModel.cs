using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGridModel : Singleton<HexGridModel>
{
    
    public static int WIDTH = 7;
    public static int HEIGHT = 9;

    public static float VIEW_HEIGHT = (HEIGHT - 0.5f) * 0.5f * HexConst.height;

    public HexGrid grid;

    public void Init()
    {
        HexNodeMarker.CreateMarkers();
    }

    public HexGrid CreateEmptyGrid()
    {
        HexGrid grid = new HexGrid();
        grid.InitSize(WIDTH, HEIGHT);

        for (int y = 0; y < grid.heightMax; y++)
        {
            List<HexNode> row = new List<HexNode>();
            grid.nodes.Add(row);
            for (int x = 0; x < grid.widthMax; x++)
            {
                HexNode node = new HexNode();
                node.originPoint = grid.originPoint;
                node.ArrayCoord = new Vector2(x, y);
                row.Add(node);
            }
        }
        grid.BuildNeighbors();
        return grid;
    }
}
