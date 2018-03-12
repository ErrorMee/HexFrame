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
        string questDir = PathUtil.GetQuestDir(type);

        string filePath = questDir + id.ToString("00000") + ".bytes";

        FileStream fs = new FileStream(filePath, FileMode.Create);

        BinaryWriter bw = new BinaryWriter(fs);

        bw.Write(id);
        
        grid.TraversalNodes((node) =>
        {
            bw.Write(node.marker.id);
        });

        fs.Flush();
        fs.Close();
        bw.Close();
    }

    /// <summary>
    /// 读取文件
    /// </summary>
    public void Load(string fileName)
    {
        FileStream fs = new FileStream(fileName, FileMode.Open);

        BinaryReader br = new BinaryReader(fs);

        MAX_ID = id = br.ReadInt32();

        grid = HexGridModel.Instance.CreateEmptyGrid();
        
        grid.TraversalNodes((node) =>
        {
            node.marker = HexNodeMarker.MARKERS[br.ReadInt32()];
        });

        fs.Close();
        br.Close();
    }
}