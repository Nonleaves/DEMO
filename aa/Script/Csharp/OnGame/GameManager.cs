using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum GameState
{
    OnGame,
    Pause,
    GameOver
}
public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    public Transform Player;
    public Transform NPCs;

    public GameState curState;

    public NPCmode npcmode;

    public GameObject npcDieEF;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        curState = GameState.OnGame;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void PauseGame()
    {
        if (curState == GameState.OnGame)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            curState = GameState.Pause;
            GameUIManager.instance.SettingPanel.SetActive(true);
        }
        else if (curState == GameState.Pause)
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            curState = GameState.OnGame;
            GameUIManager.instance.SettingPanel.SetActive(false);
        }

    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        Invoke("LoadScene",0.2f);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Invoke("LoadScene_Game", 0.2f);
    }

    private void LoadScene()
    {
        
        GameUIManager.instance. lua.Dispose();
        GameUIManager.instance.lua = null;
        GameUIManager.instance.gameOverIm.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        LoadAB.instance.uniqueClipsAB.Unload(true);
        LoadAB.instance.stepClipsAB.Unload(true);
        SceneManager.LoadScene("LoginScene");
    }
    private void LoadScene_Game()
    {
        GameUIManager.instance.lua.Dispose();
        GameUIManager.instance.lua = null;
        GameUIManager.instance.gameOverIm.SetActive(false);
        curState = GameState.OnGame;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("Game_1");
    }
    public void GameOer()
    {
        Time.timeScale = 0f;
        curState = GameState.GameOver;
        Cursor.lockState = CursorLockMode.None;
        GameUIManager.instance.gameOverIm.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
