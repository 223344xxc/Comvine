using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    PlayerCubeCtrl cube;

    private void Awake()
    {
        cube = transform.GetComponentInParent<PlayerCubeCtrl>();
    }

    public void DestroyCube()
    {
        cube.RemoveCube();
    }
}
