using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    public Transform saveScrollViewContent;
    public GameObject saveItemPrefab;


    
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public void ReadAllSavesFromLocal()
    {
        
        FileInfo[] fileInfos = FileUtil.ReadAllFiles(SaveSystem.path);
        for (int index = 0; index<fileInfos.Length; index++)
        {
            FileInfo fileInfo = fileInfos[index];
            string fileName = fileInfo.Name;
            DateTime modifyTime = fileInfo.CreationTime;
            Transform saveItem = Instantiate(saveItemPrefab,saveScrollViewContent).transform;

            saveItem.GetChild(0).GetComponent<Text>().text = fileName;
            saveItem.GetChild(1).GetComponent<Text>().text = modifyTime.ToString("M") + " " + modifyTime.ToString("t");
        }
    }

    public void SaveNewFile()
    {
        Transform Npcs = GameManager.instance.NPCs;
        BaseHumanData[] npcBaseDatas = new BaseHumanData[Npcs.childCount];
        for (int i=0;i<npcBaseDatas.Length;i++)
        {
            npcBaseDatas[i] = Npcs.GetChild(i).GetComponent<NPCController>().baseData;
        }
        string fileName = "";
        DateTime modifyTime = DateTime.Now;
        Transform player = GameManager.instance.Player;
        BaseHumanData playerData = player.GetComponent<PlayerController>().baseData;
        SaveSystem.CreateSave(playerData,npcBaseDatas,out fileName,out modifyTime);

        Transform saveItem = Instantiate(saveItemPrefab, saveScrollViewContent).transform;

        saveItem.GetChild(0).GetComponent<Text>().text = fileName;
        saveItem.GetChild(1).GetComponent<Text>().text = modifyTime.ToString("M") + " " + modifyTime.ToString("t");
    }

    public void LoadFile(string fileName)
    {
        List<BaseHumanData> allDatas = SaveSystem.LoadAllData(fileName);
        // 注册玩家信息
        GameManager.instance.Player.GetComponent<PlayerController>().RegisterData(allDatas[0]);
        // 注册敌人信息
        Transform Npcs = GameManager.instance.NPCs;
        for (int i =1;i<allDatas.Count;i++)
        {
            Npcs.GetChild(i - 1).GetComponent<NPCController>().RegisterData(allDatas[i]);
        }
    }
}
