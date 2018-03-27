using System;
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

    /// <summary>
    /// 要走这么多步才能完成目标~
    /// </summary>
    public int allStep = 0;

    /// <summary>
    /// 难度系数3.0
    /// </summary>
    public float rating;

    /// <summary>
    /// 条条大路通罗马
    /// </summary>
    public List<List<HexNode>> routes = new List<List<HexNode>>();

    /// <summary>
    /// 终止点
    /// </summary>
    public HexNode endNode;

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


    /// <summary>
    /// 查询路线
    /// </summary>
    /// <param name="tryNode"></param>
    /// <param name="tryMarker"></param>
    /// <returns></returns>
    public int QueryRoute(HexNode tryNode = null, HexNodeMarker tryMarker = null)
    {
        HexNodeMarker backupMarker = null;
        if (tryNode != null && tryMarker != null)
        {
            backupMarker = tryNode.marker;
            tryNode.marker = tryMarker;
        }

        List<HexNode> startNodes = GetNodesByType(HexNodeType.ORIGIN);
        if (startNodes.Count < 1)
        {
            if (tryNode != null && backupMarker != null)
            {
                tryNode.marker = backupMarker;
            }
            return int.MaxValue;
        }

        endNode = null;
        List<HexNode> endNodes = GetNodesByType(HexNodeType.END);
        if (endNodes.Count > 1)
        {//终点不可以大于一个
            if (tryNode != null && backupMarker != null)
            {
                tryNode.marker = backupMarker;
            }
            return 0;
        }
        else if(endNodes.Count == 1)
        {
            endNode = endNodes[0];
        }

        allStep = GetSteps();
        Reset();
        routes = new List<List<HexNode>>();
        for (int i = 0;i< startNodes.Count;i++)
        {
            HexNode startNode = startNodes[i];
            RecursiveRoute(startNode);
        }

        if (tryNode != null && backupMarker != null)
        {
            tryNode.marker = backupMarker;
        }
        rating = (allStep + 1) * (allStep + 1) / (routes.Count + 1);
        return routes.Count;
    }

    private void RecursiveRoute(HexNode node)
    {
        List<HexNode> route = node.GetRoute();

        List<HexNode> leftNeighbors = node.GetLeftNeighbors(route);
        
        if (route.Count == allStep)
        {
            if (endNode != null)
            {
                if (endNode == node)
                {
                    routes.Add(route);
                }
            }
            else
            {
                routes.Add(route);
            }
            return;
        }

        for (int n = 0; n < leftNeighbors.Count; n++)
        {
            HexNode neighbor = leftNeighbors[n];
            if (neighbor.marker.nodeType == HexNodeType.ORDER)
            {//顺序点处理
                HexNode lastOrderNode = node.GetLastOrderInRoute(route);
                if (lastOrderNode != null)
                {
                    if (lastOrderNode.marker.order + 1 != neighbor.marker.order)
                    {
                        continue;
                    }
                }
            }
            if (neighbor.marker.nodeType == HexNodeType.FREQUENCY)
            {
                if (route.Contains(neighbor))
                {
                    node.ToAdd(neighbor);
                }
                else
                {
                    node.To(neighbor);
                }
            } else
            {
                node.To(neighbor);
            }
            
            RecursiveRoute(neighbor);
        }
    }

    public int GetSteps()
    {
        int steps = 0;
        TraversalNodes((node) =>
        {
            if (node.marker.nodeType != HexNodeType.NONE)
            {
                steps += (int)node.marker.frequency;
            }
        });
        return steps;
    }

    public List<HexNode> GetNodesByType(HexNodeType type)
    {
        List<HexNode> nodes = new List<HexNode>();
        TraversalNodes((node) =>
        {
            if (node.marker.nodeType == type)
            {
                nodes.Add(node);
            }
        });
        return nodes;
    }

    public void Reset()
    {
        TraversalNodes((node) =>
        {
            node.Reset();
        });
    }
}
