using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCtrl : MonoBehaviour
{
    public GameObject popup;
    public bool onPopup = false;    

    private void Awake()
    {
        popup.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPopup();
        }
    }

    public void SetPopup()
    {
        onPopup = !onPopup;
        popup.SetActive(onPopup);
    }

    public void ExitGame()
    {
        Application.Quit();

    }
}
