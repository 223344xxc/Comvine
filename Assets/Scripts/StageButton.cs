using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    private Text text;
    private Image image;
    public int StageNumber;

    private void Start()
    {
        text = transform.GetComponentInChildren<Text>();
        image = GetComponent<Image>();

        if (PlayerPrefs.GetInt("Level_" + StageNumber) > 0)
        {
            image.sprite = Resources.Load<Sprite>("Image/clear_button");
        }
    }
}
