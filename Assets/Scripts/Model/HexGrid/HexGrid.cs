using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HexGrid
{
    /// <summary>
    /// 所有节点
    /// 原则上支持每行个数不一致
    /// </summary>
    public List<List<HexNode>> nodes = new List<List<HexNode>>();

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
    /// 原点居中
    /// </summary>
    public void OriginCenter()
    {
        originPoint = new Vector2(-(widthMax - 1) * 0.5f, -(heightMax - 1) * 0.5f);
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


}
