using System.IO;

/// <summary>
/// 关卡类型
/// </summary>
public enum HexQuestType : int
{
    MAIN = 1,
    CUSTOM = 2,
}

public class HexQuest
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
    /// 
    /// </summary>
    public HexQuestType type;

    public HexGrid grid;

    public HexQuest()
    {
        id = ++ MAX_ID;
    }

    /// <summary>
    /// 保存关卡
    /// </summary>
    public void Save()
    {
        string fileDir;
#if UNITY_EDITOR
        fileDir = PathUtil.StreamingassetsPath + "/HexQuest/" + type.ToString() +  "/";
#else
        fileDir = PathUtil.PatchPath + "/HexQuest/" + type.ToString() +  "/";
#endif
        string filePath = fileDir + id + ".bytes";
        
        if (!Directory.Exists(fileDir))
        {
            Directory.CreateDirectory(fileDir);
        }
        

        FileStream fs = new FileStream(filePath, FileMode.Create);

        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(id);

        bw.Write(grid.widthMax);
        bw.Write(grid.heightMax);

        grid.TraversalNodes((node) =>
        {
            bw.Write(node.marker.id);
        });

        fs.Flush();
        fs = null;
        bw = null;
    }
}