using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public void TryStageLoad(int stageNum)
    {
        SceneLoader.StartLoadScene("Level_"+stageNum);
    }

    public void Start()
    {
        SceneLoader.SuccessLoading();
    }
}
