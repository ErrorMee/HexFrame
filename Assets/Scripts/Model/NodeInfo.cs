using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    NONE = 0,       //空白区域
    ORIGIN = 1,     //起点
    NORM = 2,       //标准点
    ORDER = 3,      //顺序点
    FREQUENCY = 4,  //频次点

}

public class NodeInfo
{

    public NodeType nodeType = NodeType.NONE;

    public Vector2 pos;


}
