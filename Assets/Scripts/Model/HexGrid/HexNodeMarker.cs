
using System.Collections.Generic;


/// <summary>
/// HexNode样例 共享数据
/// </summary>
public class HexNodeMarker
{
    /// <summary>
    /// 样例列表
    /// </summary>
    public static List<HexNodeMarker> MARKERS;

    /// <summary>
    /// 最大ID
    /// </summary>
    private static int MAX_ID;

    /// <summary>
    /// 唯一ID
    /// </summary>
    public int id;

    /// <summary>
    /// 节点类型
    /// </summary>
    public HexNodeType nodeType = HexNodeType.NONE;

    /// <summary>
    /// 控制触摸顺序
    /// </summary>
    public HexNodeOrder order = HexNodeOrder.NONE;

    /// <summary>
    /// 触摸次数
    /// </summary>
    public HexNodeFrequency frequency = HexNodeFrequency.DEFAULT;

    /// <summary>
    /// 入口
    /// </summary>
    public List<HexNodeDir> entrances = new List<HexNodeDir> { HexNodeDir.ALL };

    /// <summary>
    /// 出口
    /// </summary>
    public List<HexNodeDir> exits = new List<HexNodeDir> { HexNodeDir.ALL };

    private HexNodeMarker()
    {
        id = ++MAX_ID;
    }

    /// <summary>
    /// 生成样例
    /// </summary>
    public static void CreateMarkers()
    {
        MARKERS = new List<HexNodeMarker>();

        HexNodeMarker node;

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

    private static HexNodeMarker CreateMarker(HexNodeType nodeType)
    {
        HexNodeMarker node = new HexNodeMarker();
        node.nodeType = nodeType;
        MARKERS.Add(node);
        return node;
    }
}