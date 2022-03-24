using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static GameObject LoadEffect;
    private static Animator anim;
    private static string nowLoadSceneName;
    private static bool nowLoading = false;

    private void Awake()
    {
        LoadEffect = GameObject.Find("LoadEffect");
        anim = LoadEffect.transform.GetComponentInChildren<Animator>();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(LoadEffect);
        LoadEffect.SetActive(false);
    }

    public static void ReloadScene()
    {
        StartLoadScene(nowLoadSceneName);
    }

    public static void StartLoadScene(string sceneName)
    {
        nowLoadSceneName = sceneName;
        LoadEffect.SetActive(true);
        anim.speed = 1;
        nowLoading = true;
    }

    public static void SaveSceneClear()
    {
        PlayerPrefs.SetInt(nowLoadSceneName, 1);
        
    }

    public static void SuccessLoading()
    {
        if (!nowLoading)
            return;
        anim.speed = 1;
    }

    public static void TryLoadScene()
    {
        SceneManager.LoadScene(nowLoadSceneName);
    }

    public static void EndLoadScene()
    {
        LoadEffect.SetActive(false);
        anim.speed = 0;
    }
}
