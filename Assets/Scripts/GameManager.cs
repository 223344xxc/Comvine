using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    None,
    PlayAction,
}

public class GameManager : MonoBehaviour
{
    public static GameObject ClearUI;
    public static GameState gameState = GameState.None;
    public static SoundManager soundManager;
    public static bool isGameClear = false;


    public void Awake()
    {
        ClearUI = GameObject.Find("ClearLayer");
        ClearUI.SetActive(false); 
        isGameClear = false;
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    public void Start()
    {
        SceneLoader.SuccessLoading();
    }

    public static void GameClear()
    {
        if (gameState == GameState.None)
        {
            gameState = GameState.PlayAction;
            isGameClear = true;
            ClearUI.SetActive(true);
            soundManager.PlaySound(SoundType.clear);
            
        }
    }

    public static void ResetGame()
    {
        SceneLoader.ReloadScene();
    }

    public static void ReturnHome()
    {
        ResetStaticObject();
        SceneLoader.StartLoadScene("StageSelect");
    }

    public static void ReturnStageSelectScene()
    {
        ResetStaticObject();
        SceneLoader.SaveSceneClear();
        SceneLoader.StartLoadScene("StageSelect");
    }
    public static void ResetStaticObject()
    {
        ClearUI = null;
        gameState = GameState.None;
    }
}
