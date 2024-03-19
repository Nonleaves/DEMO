using LuaInterface;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour 
{

    Color weakP = new Color(0.9f,0.4f,0.4f);

    public static GameUIManager instance;

    public Transform PlayerHPbar;
    public Transform NPCs;

    [Header("设置界面")]
    public GameObject SettingPanel;

    public Transform saveToggle;
    public Transform generalToggle;

    public GameObject saveIm;
    public GameObject generalIm;

    public GameObject gameOverIm;

    [Header("存档系统界面")]
    public GameObject saveViewContent;

    [Header("通用设置界面")]
    public GameObject generalView;
    public Text bmgText;
    public Text esText;

    [Header("音频播放器")]
    public AudioSource playerAS;
    public AudioSource playerStepAS;
    public AudioSource audioManager;

    public AudioSource bgm;

    public LuaState lua = null;
    public LuaFunction luaFunc = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SettingPanel.SetActive(false);
        new LuaResLoader();
        lua = new LuaState();
        lua.Start();
        LuaBinder.Bind(lua);
        string luaPath = Application.dataPath + "/Script/Lua";
        lua.AddSearchPath(luaPath);
        lua.DoFile("OnGame.lua");

        UpdateAllHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearAllSaveItem();
            GameManager.instance.PauseGame();
            ChangeSettingView();
            SaveManager.instance.ReadAllSavesFromLocal();
        }

    }

    private void ClearAllSaveItem()
    {
        int itemsCount = saveViewContent.transform.childCount;
        for (int i=0;i<itemsCount;i++)
        {
            Destroy(saveViewContent.transform.GetChild(i).gameObject);
        }
    }


    public void UpdateAllHpBar()
    {
        BaseHumanData playerData = GameManager.instance.Player.GetComponent<PlayerController>().baseData;
        float restHP = (float)(playerData.curHP) / (float)(playerData.maxHP);
        //float v = 0.5f;
        //PlayerHPbar.GetChild(0).GetComponent<Image>().fillAmount = Mathf.SmoothDamp(PlayerHPbar.GetChild(1).GetComponent<Image>().fillAmount,restHP,ref v,1f);
        PlayerHPbar.GetChild(1).GetComponent<Image>().fillAmount = restHP;

        int npcCount = NPCs.childCount;
        for (int i =0;i<npcCount;i++)
        {
            Transform singleNpc = NPCs.GetChild(i);
            BaseHumanData npcData = singleNpc.GetComponent<NPCController>().baseData;
            float npcRestHP = (float)(npcData.curHP) / (float)(npcData.maxHP);
            //singleNpc.GetComponent<NPCController>().HPbar.GetChild(0).GetComponent<Image>().fillAmount = Mathf.SmoothDamp(singleNpc.GetComponent<NPCController>().HPbar.GetChild(1).GetComponent<Image>().fillAmount, npcRestHP, ref v, 1f);
            singleNpc.GetComponent<NPCController>().HPbar.GetChild(1).GetComponent<Image>().fillAmount = npcRestHP;
        }
    }
    


    public void BMGSliderChange(float value)
    {
        bgm.volume = value;
        bmgText.text = ((int)(value * 100)).ToString();
    }
    public void ESSliderChange(float value)
    {
        for (int i=0;i< NPCs.childCount;i++)
        {
            NPCs.GetChild(i).GetComponent<AudioSource>().volume = value;
            NPCs.GetChild(i).GetChild(0).GetComponent<AudioSource>().volume = value;
        }
        playerStepAS.volume = value;
        playerAS.volume = value;
        audioManager.volume = value;
        esText.text = ((int)(value * 100)).ToString();
    }

    public void AutoToggleValueChange(bool isOn)
    {
        if (isOn)
        {
            GameManager.instance.npcmode = NPCmode.FOLLOW;
        }
        else
        {
            GameManager.instance.npcmode = NPCmode.STAY;
        }
        
    }
    public void SaveToggleValueChange(bool isOn)
    {
        saveIm.SetActive(isOn);
        if (isOn)
        {
            ClearAllSaveItem();
            SaveManager.instance.ReadAllSavesFromLocal();
        }
        ChangeSettingView();
    }

    public void GeneralToggleValueChange(bool isOn)
    {
        generalIm.SetActive(isOn);
        ChangeSettingView();
    }

    private void ChangeSettingView()
    {
        if (saveToggle.GetComponent<Toggle>().isOn)
        {
            saveToggle.GetChild(0).GetComponent<Image>().color = weakP;
            generalToggle.GetChild(0).GetComponent<Image>().color = Color.white;
        }
        else
        {
            saveToggle.GetChild(0).GetComponent<Image>().color = Color.white;
            generalToggle.GetChild(0).GetComponent<Image>().color = weakP;
        }
    }

    public void FullScreenToggleValueChange(bool isOn)
    {
        if (isOn)
        {
            Screen.fullScreen = isOn;
            Screen.SetResolution(1920, 1080, false);
        }
        else
        {
            Screen.fullScreen = isOn;
            Screen.SetResolution(1280, 720, false);
        }
    }


}
