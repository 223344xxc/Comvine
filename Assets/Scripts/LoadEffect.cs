using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadEffect : MonoBehaviour
{
    public Animator anim;
    public void PauseAnim()
    {
        anim.speed = 0;
        SceneLoader.TryLoadScene();
    }

    public void EndAnim()
    {
        SceneLoader.EndLoadScene();
    }
}
