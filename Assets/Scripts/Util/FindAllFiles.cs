using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class FindAllFiles
{
    private static List<string> paths = new List<string>();
    private static List<string> files = new List<string>();

	public static List<string> ListFiles(string rootPath, bool isRecursive = true, Action<string> fileCallback = null, Action<string> dirCallback = null)
    {
        paths.Clear();
        files.Clear();
        Recursive(rootPath, isRecursive, fileCallback, dirCallback);
        return files;
    }

    //递归遍历文件和文件夹
    private static void Recursive(string path, bool isRecursive, Action<string> fileCallback, Action<string> dirCallback)
    {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names)
        {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            string addFileName = filename.Replace('\\', '/');
            files.Add(addFileName);
            if (fileCallback != null)
            {
                fileCallback(addFileName);
            }
        }
        foreach (string dir in dirs)
        {
            string addDirName = dir.Replace('\\', '/');
            paths.Add(addDirName);
            if (dirCallback != null)
            {
                dirCallback(addDirName);
            }
            if (isRecursive)
            {
                Recursive(dir, isRecursive, fileCallback, dirCallback);
            }
        }
    }
}