using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomQuest
{
    /// <summary>
    /// 最大ID
    /// </summary>
    private static int MAX_ID;

    /// <summary>
    /// 唯一ID
    /// </summary>
    public int id;

    public HexGrid grid;

    public CustomQuest()
    {
        id = ++ MAX_ID;
    }
}