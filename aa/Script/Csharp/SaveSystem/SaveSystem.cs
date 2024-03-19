using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveSystem 
{
    public static string path = Application.persistentDataPath + "/saves/";
    public static void CreateSave(BaseHumanData playerData,BaseHumanData[] allNpcDatas,out string fileName,out DateTime createTime)
    {
        FileInfo[] allFilesInfos = FileUtil.ReadAllFiles(path);
        string num = "";
        if (allFilesInfos.Length==0)
        {
            num = (GameUIManager.instance.saveViewContent.transform.childCount + 1).ToString();
        }
        else
        {
            string lastFileName = allFilesInfos[allFilesInfos.Length - 1].Name;
            num = (int.Parse(lastFileName.Split('_')[2])+1).ToString();
        }
        
        fileName = "Save_Data_" + num + "_.data";
        createTime = DateTime.Now;
        Debug.Log(fileName);
        SaveAllData(playerData, allNpcDatas, fileName);

    }
    public static void SaveAllData(BaseHumanData playerData, BaseHumanData[] allNpcDatas, string fileName)
    {
        // 创建StreamWriter
        StreamWriter writer = new StreamWriter(path+fileName);
        // 将数据转化为JSON字符串
        string JSData = JsonUtility.ToJson(playerData);
        foreach(var item in allNpcDatas)
        {
            JSData +='|'+JsonUtility.ToJson(item);
        }
        writer.Write(JSData);
        writer.Close();

    }
    public static List<BaseHumanData> LoadAllData(string fileName)
    {
        if (File.Exists(path+fileName))
        {
            StreamReader sr = new StreamReader(path+fileName);
            string JSData = sr.ReadToEnd();
            string[] allSplitData = JSData.Split('|');
            //BaseHumanData[] allBaseData = new BaseHumanData[allSplitData.Length];
            List<BaseHumanData> allData = new List<BaseHumanData>();
            for (int i = 0; i < allSplitData.Length; i++)
            {
                if (i == 0)
                {
                    BaseHumanData playerData = ScriptableObject.CreateInstance<BaseHumanData>();
                    JsonUtility.FromJsonOverwrite(allSplitData[i], playerData);
                    allData.Add(playerData);
                }
                else
                {
                    BaseHumanData npcData = ScriptableObject.CreateInstance<BaseHumanData>();
                    JsonUtility.FromJsonOverwrite(allSplitData[i], npcData);
                    allData.Add(npcData);
                }
            }
            return allData;
        }
        else
        {
            Debug.LogError("File Not Found");
            return null;
        }
    }


    

    
}
