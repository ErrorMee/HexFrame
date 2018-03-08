using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGridModel : Singleton<HexGridModel>
{
    /// <summary>
    /// 六边形节点样例
    /// </summary>
    public List<HexNode> hexNodeMarkers = new List<HexNode>();

    public HexGrid grid;

    public void Init()
    {
        CreateMarkers();
    }


    /// <summary>
    /// 生成样例
    /// </summary>
    private void CreateMarkers()
    {
        HexNode node;

        //HexNodeType.NONE
        node = CreateMarker(HexNodeType.NONE);

        //HexNodeType.ORIGIN
        node = CreateMarker(HexNodeType.ORIGIN);

        //HexNodeType.NORM
        node = CreateMarker(HexNodeType.NORM);

        //HexNodeType.ORDER
        node = CreateMarker(HexNodeType.ORDER);
        node.order = HexNodeOrder.FIRST;

        node = CreateMarker(HexNodeType.ORDER);
        node.order = HexNodeOrder.SECOND;

        //HexNodeType.FREQUENCY
        node = CreateMarker(HexNodeType.FREQUENCY);
        node.frequency = HexNodeFrequency.DOUBLE;

        //HexNodeType.BRIDGE 
        //南>北 单通桥
        node = CreateMarker(HexNodeType.BRIDGE);
        node.entrances = new List<HexNodeDir> { HexNodeDir.SOUTH };
        node.exits = new List<HexNodeDir> { HexNodeDir.NORTH };
        //南<>北 互通桥
        node = CreateMarker(HexNodeType.BRIDGE);
        node.entrances = new List<HexNodeDir> { HexNodeDir.SOUTH, HexNodeDir.NORTH };
        node.exits = new List<HexNodeDir> { HexNodeDir.SOUTH, HexNodeDir.NORTH };
        //动态 单通桥
        node = CreateMarker(HexNodeType.BRIDGE);
        node.entrances = new List<HexNodeDir> { HexNodeDir.ALL };
        node.exits = new List<HexNodeDir> { HexNodeDir.OPPOSITE };

    }

    private HexNode CreateMarker(HexNodeType nodeType)
    {
        HexNode node = new HexNode();
        node.nodeType = nodeType;
        hexNodeMarkers.Add(node);
        return node;
    }

    public HexGrid CreateEmptyGrid(int width, int height)
    {
        HexGrid grid = new HexGrid();
        grid.InitSize(width, height);

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
