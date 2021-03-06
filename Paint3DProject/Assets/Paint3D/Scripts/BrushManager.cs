﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BrushManager {
    //                  name 
    private static List<string> availableBrushes;
    public static List<string> AvailableBrushes
    {
        get
        {
            if (availableBrushes == null)
            {
                availableBrushes = new List<string>();
                availableBrushes.Add("LineBrush");
                availableBrushes.Add("BubbleBrush");
                availableBrushes.Add("TubeBrush");
            }
            return availableBrushes;
        }
    }

    public static IBrush CreateBrush(Stroke stroke, string name, Dictionary<string, object> options)
    {
        // try and create the brush
        // if unable to instantiat, return LineBrush by default 
        IBrush o = (ScriptableObject.CreateInstance(name) as IBrush) ?? ScriptableObject.CreateInstance<LineBrush>();
        o.Stroke = stroke;
        return o;
    }

    public static Dictionary<string, object> GetDefaultOptions(string brushName)
    {
        IBrush o = (ScriptableObject.CreateInstance(brushName) as IBrush);
        if (o == null)
        {
            return null;
        }

        return o.GetOptions();
    }
}
