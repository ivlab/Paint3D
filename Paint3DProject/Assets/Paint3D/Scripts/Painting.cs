using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Painting : MonoBehaviour {

    public List<Stroke> strokes;
    public int curIndex;
    public Stroke curStroke;

	// Use this for initialization
	void Start () {
        strokes = new List<Stroke>();
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < strokes.Count; i++)
        {
            strokes[i].Draw();
        }
	}
}
