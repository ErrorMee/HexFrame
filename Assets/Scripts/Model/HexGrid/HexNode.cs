using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode
{
    /// <summary>
    /// 最大ID
    /// </summary>
    private static int MAX_ID;

    /// <summary>
    /// 唯一ID
    /// </summary>
    public int id;

    /// <summary>
    /// 样例
    /// </summary>
    public HexNodeMarker marker = HexNodeMarker.MARKERS[0];

    /// <summary>
    /// 偏移位置
    /// </summary>
    public Vector2 originPoint;

    /// <summary>
    /// 二维数组坐标
    /// </summary>
    private Vector2 arrayCoord;
    public Vector2 ArrayCoord {
        get { return arrayCoord; }
        set
        {
            arrayCoord = value;
            hexCoord = HexCoordinates.FromArrayCoordinates((int)arrayCoord.x, (int)arrayCoord.y);
        }
    }
    
    /// <summary>
    /// 六边形坐标
    /// </summary>
    public HexCoordinates hexCoord;
    
    /// <summary>
    /// 邻居们燥起来！
    /// </summary>
    public HexNode[] neighbors = new HexNode[6];

    /// <summary>
    /// 你从哪里来？
    /// </summary>
    public HexNode from;

    /// <summary>
    /// 你要去哪呀？
    /// </summary>
    public HexNode to;

    /// <summary>
    /// 追溯路径
    /// </summary>
    public List<HexNode> route;

    public HexNode()
    {
        id = ++MAX_ID;
    }

    public void Reset()
    {
        from = null;
        to = null;
        route = null;
    }

    /// <summary>
    /// 设置邻居
    /// </summary>
    /// <param name="neighbor"></param>
    /// <param name="dir"></param>
    public void SetNeighbor(HexNode neighbor,int hexNodeDir)
    {
        neighbors[hexNodeDir - 1] = neighbor;
        if (neighbor != null)
        {
            if (hexNodeDir <= (int)HexNodeDir.SOUTH_EAST)
            {
                neighbor.SetNeighbor(this, hexNodeDir + (int)HexNodeDir.SOUTH_EAST);
            }
        }
    }

    /// <summary>
    /// 在家的都来轰趴吧
    /// </summary>
    /// <returns></returns>
    public List<HexNode> GetHomeNeighbors()
    {
        List<HexNode> homeNeighbors = new List<HexNode>();
        for (int n = 0; n < neighbors.Length; n++)
        {
            HexNode neighbor = neighbors[n];
            if (neighbor != null && neighbor.marker.nodeType != HexNodeType.NONE)
            {
                homeNeighbors.Add(neighbor);
            }
        }
        return homeNeighbors;
    }

    /// <summary>
    /// 每家送个大苹果 不能多也不能少
    /// </summary>
    /// <returns></returns>
    public List<HexNode> GetLeftNeighbors()
    {
        List<HexNode> route = GetRoute();

        List<HexNode> homeNeighbors = new List<HexNode>();
        for (int n = 0; n < neighbors.Length; n++)
        {
            HexNode neighbor = neighbors[n];
            if (neighbor != null && neighbor.marker.nodeType != HexNodeType.NONE)
            {
                if (!route.Contains(neighbor))
                {
                    homeNeighbors.Add(neighbor);
                }
                else
                {
                    //todo 多次访问的
                }
            }
        }
        return homeNeighbors;
    }

    public void To(HexNode node)
    {
        node.from = this;
        this.to = node;
    }

    public void From(HexNode node)
    {
        this.from = node;
        node.to = this;
    }

    /// <summary>
    /// 获取路径深度
    /// </summary>
    /// <returns></returns>
    public List<HexNode> GetRoute()
    {
        route = new List<HexNode>();
        RecursiveRoute(this, route);
        return route;
    }

    private void RecursiveRoute(HexNode node, List<HexNode> route)
    {
        route.Add(node);
        if (node.from != null)
        {
            RecursiveRoute(node.from, route);
        }
    }

    /// <summary>
    /// 可视化的坐标
    /// </summary>
    /// <returns></returns>
    public Vector2 GetViewCoord()
    {
        Vector2 viewCoord = new Vector2();

        viewCoord.x = (arrayCoord.x + originPoint.x) * (HexConst.outerRadius * 1.5f);
        viewCoord.y = ((arrayCoord.y + originPoint.y) + arrayCoord.x % 2 * 0.5f) * (HexConst.innerRadius * 2);

        return viewCoord;
    }

    public string ArrayPosString()
    {
        return (ArrayCoord.x + originPoint.x) + "," + (ArrayCoord.y + originPoint.y);
    }

    public string HexPosString()
    {
        return hexCoord.PosString(originPoint);
    }
}

public struct HexCoordinates
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public int Z
    {
        get
        {
            return -X - Y;
        }
    }

    public HexCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public string PosString(Vector2 originPoint)
    {
        return (X + originPoint.x) + "," + (Y + originPoint.y - originPoint.x / 2) + "," 
            + ((X + originPoint.x) + (Y + originPoint.y - originPoint.x / 2));
    }

    public static HexCoordinates FromArrayCoordinates(int x, int y)
    {
        return new HexCoordinates(x, y - x / 2);
    }
}