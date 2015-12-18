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
    public string CurrentBrush { get; set; }

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

        foreach (string name in BrushManager.AvailableBrushes)
        {
            Dictionary<string, object> defaultOpt = BrushManager.GetDefaultOptions(name);
            Dictionary<string, object> opt = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> pair in defaultOpt)
            {
                opt.Add(pair.Key, pair.Value);
            }
            options.Add(name, opt);
        }
    }
	
	// Update is called once per frame
	void Update () {
        //for (int i = 0; i < strokes.Count; i++)
        //{
        //    strokes[i].Draw();
        //}
	}

    public void StartNewStroke()
    {
        if (CurrentStroke == null)
        {
            CurrentStroke = gameObject.AddComponent<Stroke>();
            strokes.Add(CurrentStroke);
            curIndex = strokes.Count - 1;
            CurrentStroke.Brush = BrushManager.CreateBrush(CurrentStroke, CurrentBrush, null);
            CurrentStroke.Brush.SetOptions(options[CurrentStroke.Brush.BrushName]); 
        }
    }

    public void AddVertuex(Vertex v)
    {
        if (CurrentStroke != null)
        {
            CurrentStroke.AddVertex(v);
        }
    }

    public void EndStroke()
    {
        CurrentStroke = null;
    }

}
