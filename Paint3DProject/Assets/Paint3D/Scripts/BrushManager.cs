using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrushManager : MonoBehaviour {
    //                  name,     options {  name, type } 
    public Dictionary<string, Dictionary<string, System.Type>> availableBrushes;

    public IBrush CreateBrush(string name, Dictionary<string, object> options)
    {
        // try and create the brush
        // if unable to instantiat, return LineBrush by default 
        IBrush o = (ScriptableObject.CreateInstance(name) as IBrush) ?? ScriptableObject.CreateInstance<LineBrush>();
        return o;
    }

    // Use this for initialization
    void Start()
    {
    }    //
	
}
