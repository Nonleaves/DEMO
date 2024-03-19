using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public static LoginManager instance;

    LuaState lua = null;
    LuaFunction luaFunc = null;

    [Header("µÇÂ¼")]
    public Text inputUsername;
    public Text inputPassword;
    public Button confirmBtn;


    [Header("ÌáÊ¾´°¿Ú")]
    public GameObject sucWd;

    public GameObject failWd;
    public GameObject reminderPanel;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        Screen.SetResolution(1920, 1080, true );
        new LuaResLoader();
        lua = new LuaState();
        lua.Start();
        LuaBinder.Bind(lua);
        string luaPath = Application.dataPath + "/Lua";
        lua.AddSearchPath(luaPath);
        lua.DoFile("Login.lua");

    }

    public void ClickLogin()
    {
        luaFunc = lua.GetFunction("Login.log");
        luaFunc.Call(inputUsername.text, inputPassword.text);
        luaFunc.Dispose();
        luaFunc = null;
    }

    public void Click2Return()
    {
        luaFunc = lua.GetFunction("Login.returnLog");
        luaFunc.Call();
        luaFunc.Dispose();
        luaFunc = null;
    }

    public void StartGame()
    {
        Invoke("LoadScene",0.2f);
    }

    private void LoadScene()
    {
        lua.Dispose();
        lua = null;
        SceneManager.LoadScene("Game_1");
    }
}
