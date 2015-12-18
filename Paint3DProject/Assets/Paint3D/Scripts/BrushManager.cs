using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BrushManager {
    //                  name 
    public static List<string> availableBrushes;

    public static IBrush CreateBrush(Stroke stroke, string name, Dictionary<string, object> options)
    {
        // try and create the brush
        // if unable to instantiat, return LineBrush by default 
        IBrush o = (ScriptableObject.CreateInstance(name) as IBrush) ?? ScriptableObject.CreateInstance<LineBrush>();
        o.Stroke = stroke;
        return o;
    }
}
