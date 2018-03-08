
using System.Collections.Generic;

public class HexBaseNode
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

    public HexBaseNode()
    {
        id = ++ MAX_ID;
    }
}