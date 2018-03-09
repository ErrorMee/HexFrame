﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexGrid
{
    /// <summary>
    /// 表格完成的最大顺序点
    /// </summary>
    public static HexNodeOrder NODE_ORDER = HexNodeOrder.NONE;

    /// <summary>
    /// 最大宽度 网格上最左到最右
    /// </summary>
    public int widthMax;

    /// <summary>
    /// 最大高度 网格上最下到最上
    /// </summary>
    public int heightMax;

    /// <summary>
    /// 起始点 或叫做偏移点 用于定位网格位置
    /// </summary>
    public Vector2 originPoint = new Vector2();

    /// <summary>
    /// 所有节点
    /// 原则上支持每行个数不一致
    /// </summary>
    public List<List<HexNode>> nodes = new List<List<HexNode>>();

    public void InitSize(int _widthMax,int _heightMax)
    {
        widthMax = _widthMax;
        heightMax = _heightMax;
        originPoint = new Vector2(-(widthMax - 1) * 0.5f, -(heightMax - 0.5f) * 0.5f);
    }

    /// <summary>
    /// 建立邻居关系
    /// </summary>
    public void BuildNeighbors()
    {
        TraversalNodes(BuildNeighborsHandle);
    }

    private void BuildNeighborsHandle(HexNode node)
    {
        int more = (int)node.ArrayCoord.x % 2;
        node.SetNeighbor(GetNodeFromArray(new Vector2(node.ArrayCoord.x, node.ArrayCoord.y + 1)), (int)HexNodeDir.NORTH);
        node.SetNeighbor(GetNodeFromArray(new Vector2(node.ArrayCoord.x + 1, node.ArrayCoord.y + more)), (int)HexNodeDir.NORTH_EAST);
        node.SetNeighbor(GetNodeFromArray(new Vector2(node.ArrayCoord.x + 1, node.ArrayCoord.y - 1 + more)), (int)HexNodeDir.SOUTH_EAST);
    }

    /// <summary>
    /// 遍历所有节点
    /// </summary>
    /// <param name="traversalCallBack"></param>
    public void TraversalNodes(Action<HexNode> traversalCallBack)
    {
        for (int column = 0; column < nodes.Count;column ++)
        {
            int rowCount = nodes[column].Count;
            for (int row = 0; row < rowCount; row++)
            {
                HexNode node = nodes[column][row];
                traversalCallBack(node);
            }
        }
    }

    /// <summary>
    /// 从数组中取出节点
    /// </summary>
    /// <param name="arrayCoord"></param>
    public HexNode GetNodeFromArray(Vector2 arrayCoord)
    {
        if (arrayCoord.x < 0 || arrayCoord.y < 0)
        {
            return null;
        }

        if (nodes.Count <= arrayCoord.y)
        {
            return null;
        }
        if (nodes[(int)arrayCoord.y].Count <= arrayCoord.x)
        {
            return null;
        }
        return nodes[(int)arrayCoord.y][(int)arrayCoord.x];
    }
}