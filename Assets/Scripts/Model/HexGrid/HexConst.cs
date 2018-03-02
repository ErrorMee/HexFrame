using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum HexNodeType:int
{
    NONE = 0,       //清除点
    ORIGIN = 1,     //起点
    NORM = 2,       //标准点
    ORDER = 3,      //顺序点
    FREQUENCY = 4,  //频次点

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
    CENTER = 0,         //中间 自己
    NORTH = 1,          //北
    NORTH_EAST = 2,     //东北
    SOUTH_EAST = 3,     //东南
    SOUTH = 4,          //南
    SOUTH_WEST = 5,     //西南
    NORTH_WEST = 6,     //西北
}

public static class HexConst
{
    //外圆半径
    public const float outerRadius = 93.5f;

    //六边形内圆和外圆半径比
    public const float innerOuterRadiusRatio = 0.866025404f;

    //内圆半径
    public const float innerRadius = outerRadius * innerOuterRadiusRatio;

}
