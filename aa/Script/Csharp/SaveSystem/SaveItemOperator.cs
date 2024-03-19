using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveItemOperator : MonoBehaviour
{
    Text saveName;
    Text saveTime;
    AudioManager saveAudioManager;

    private void Start()
    {
        saveAudioManager = GameObject.Find("AudioManager").transform.GetComponent<AudioManager>();
        saveName = transform.GetChild(0).GetComponent<Text>();
        saveTime = transform.GetChild(1).GetComponent<Text>();
    }




    public void LoadSave()
    {
        saveAudioManager.DoUIClick();
        Debug.Log("Load");
        string fileName = saveName.text;
        SaveManager.instance.LoadFile(fileName);
    }
    public void OverrideSave()
    {
        saveAudioManager.DoUIClick();
        string fileName = saveName.text;

        Transform Npcs = GameManager.instance.NPCs;
        BaseHumanData[] npcBaseDatas = new BaseHumanData[Npcs.childCount];
        for (int i = 0; i < npcBaseDatas.Length; i++)
        {
            npcBaseDatas[i] = Npcs.GetChild(i).GetComponent<NPCController>().baseData;
        }

        DateTime modifyTime = DateTime.Now;
        Transform player = GameManager.instance.Player;
        BaseHumanData playerData = player.GetComponent<PlayerController>().baseData;

        SaveSystem.SaveAllData(playerData,npcBaseDatas,fileName);
        saveTime.text = modifyTime.ToString("M") + " " + modifyTime.ToString("t");
    }
    public void DeleteSave()
    {
        saveAudioManager.DoUIClick();
        if (File.Exists(SaveSystem.path+ saveName.text))
        {
            File.Delete(SaveSystem.path + saveName.text);
        }
        Destroy(transform.gameObject);
    }
}
