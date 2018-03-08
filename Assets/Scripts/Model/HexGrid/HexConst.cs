
public enum HexNodeType:int
{
    /// <summary>
    /// 清除点 空点
    /// </summary>
    NONE = 0,       
    /// <summary>
    /// 启动点
    /// </summary>
    ORIGIN = 1,     
    /// <summary>
    /// 标准点 通过点
    /// </summary>
    NORM = 2,
    /// <summary>
    /// 顺序点
    /// </summary>
    ORDER = 3,
    /// <summary>
    /// 次数点
    /// </summary>
    FREQUENCY = 4,
    /// <summary>
    /// 桥 （只有两个开口 固定 和 动态）难度深挖点
    /// </summary>
    BRIDGE = 5,
}

/// <summary>
/// 触摸顺序
/// </summary>
public enum HexNodeOrder : int
{
    /// <summary>
    /// 不做限制点
    /// </summary>
    NONE = 0,
    FIRST = 1,
    SECOND = 2,
    THIRD = 3,
    FOURTH = 4,
}

/// <summary>
/// 触摸次数
/// </summary>
public enum HexNodeFrequency : int
{
    DEFAULT = 1,
    DOUBLE = 2,
    THIBLE = 3,
}

/// <summary>
/// 框架采用的是横放六边形不是竖放
///     ———
///   /     \
///   \     /
///     ———
/// </summary>
public enum HexNodeDir
{
    /// <summary>
    /// 没有方向
    /// </summary>
    NONE = -1,
    /// <summary>
    /// 中心
    /// </summary>
    CENTER = 0,

    NORTH = 1,
    /// <summary>
    /// 东北
    /// </summary>
    NORTH_EAST = 2,
    /// <summary>
    /// 东南
    /// </summary>
    SOUTH_EAST = 3,
    SOUTH = 4,
    /// <summary>
    /// 西南
    /// </summary>
    SOUTH_WEST = 5,
    /// <summary>
    /// 西北
    /// </summary>
    NORTH_WEST = 6,

    /// <summary>
    /// 反方向
    /// </summary>
    OPPOSITE = 99,
    /// <summary>
    /// 所有方向
    /// </summary>
    ALL = 100,
}

public static class HexConst
{
    //外圆半径
    public const float outerRadius = 93.5f;

    //六边形内圆和外圆半径比
    public const float innerOuterRadiusRatio = 0.866025404f;

    //内圆半径
    public const float innerRadius = outerRadius * innerOuterRadiusRatio;

    public const float width = outerRadius * 2;
    public const float height = innerRadius * 2;
}
