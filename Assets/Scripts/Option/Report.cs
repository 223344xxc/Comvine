using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Report
{
#if UNITY_EDITOR
    public static void Log(this object ob)
    {
        Debug.Log(ob);
    }

    public static void LogError(this object ob)
    {
        Debug.LogError(ob);
    }
#endif
}
