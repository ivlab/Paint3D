using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Painting : MonoBehaviour {

    public List<Stroke> strokes;
    public int curIndex;
    public Stroke CurrentStroke { get; set; }
    public string SelectedBrush { get; set; }

    // Use this for initialization
    void Start () {
        strokes = new List<Stroke>();
	}
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < strokes.Count; i++)
        //{
        //    strokes[i].Draw();
        //}
	}

    void StartNewStroke(Dictionary<string, object>)
    {
        CurrentStroke = new Stroke();
        strokes.Add(CurrentStroke);
        curIndex = strokes.Count - 1;
        CurrentStroke.Brush = BrushManager.CreateBrush(SelectedBrush, null);
    }

    void test()
    {
        Dictionary<string, System.Type> opt = BrushManager.availableBrushes["Bubbles"];
        
    
    }
}
