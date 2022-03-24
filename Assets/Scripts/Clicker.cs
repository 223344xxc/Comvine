using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clicker : MonoBehaviour
{
    public Action clickAction;

   public void OnClick()
    {
        clickAction?.Invoke();
    }
}
