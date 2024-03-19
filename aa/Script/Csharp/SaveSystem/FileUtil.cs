using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class FileUtil
{
    public static FileInfo[] ReadAllFiles(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        DirectoryInfo root = new DirectoryInfo(path);
        return root.GetFiles();
    }
}
