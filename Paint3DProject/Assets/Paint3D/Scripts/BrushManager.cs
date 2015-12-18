using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BrushManager {
    //                  name,     options {  name, type } 
    public static Dictionary<string, Dictionary<string, System.Type>> availableBrushes;

    public static IBrush CreateBrush(string name, Dictionary<string, object> options)
    {
        // try and create the brush
        // if unable to instantiat, return LineBrush by default 
        IBrush o = (ScriptableObject.CreateInstance(name) as IBrush) ?? ScriptableObject.CreateInstance<LineBrush>();
        return o;
    }
}
