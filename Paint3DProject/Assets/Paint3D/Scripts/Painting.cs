using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class hold all the strokes for one user.
/// When a new stroke is started (i.e. the user presses the main button), call StartNewStroke()
/// </summary>
public class Painting : MonoBehaviour {

    /// <summary>
    /// List of all strokes created, including the current
    /// </summary>
    public List<Stroke> strokes;
    /// <summary>
    /// index of current stroke in list 
    /// </summary>
    public int curIndex;
    /// <summary>
    /// The current stroke being drawn. <b>null</b> if nothing is being drawn.
    /// </summary>
    public Stroke CurrentStroke { get; set; }

    /// <summary>
    /// The name of the currently selected brush.
    /// </summary>
    public string SelectedBrush { get; set; }

    /// <summary>
    /// This dictionary follows the structure layed out in Brushes.txt
    /// Outer Dictionary has Key = name of brush; Value = Dictionary of options
    ///     This dictionary has Key = name of option, Value = value of option
    ///     
    /// </summary>
    public Dictionary<string, Dictionary<string, object>> options;

    // Use this for initialization
    void Start () {
        strokes = new List<Stroke>();

        options = new Dictionary<string, Dictionary<string, object>>();
        // LineBrush
        Dictionary<string, object> opt = new Dictionary<string, object>();
        opt.Add("StartColor", Color.red);
        opt.Add("EndColor", Color.magenta);
        opt.Add("StartWidth", 2.0f);
        opt.Add("EndWidth", 1.0f);
        options.Add("LineBrush", opt);

        // Tube Brush
        opt = new Dictionary<string, object>();
        opt.Add("StartColor", Color.blue);
        opt.Add("EndColor", Color.clear);
        opt.Add("StartWidth", 2.0f);
        opt.Add("EndWidth", 0.5f);
    }
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < strokes.Count; i++)
        //{
        //    strokes[i].Draw();
        //}
	}

    void StartNewStroke()
    {
        CurrentStroke = new Stroke();
        strokes.Add(CurrentStroke);
        curIndex = strokes.Count - 1;
        CurrentStroke.Brush = BrushManager.CreateBrush(CurrentStroke, SelectedBrush, null);
        CurrentStroke.Brush.SetOptions(options[CurrentStroke.Brush.BrushName]);
    }

    void EndStroke()
    {

    }

}
