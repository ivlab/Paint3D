using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct Vertex
{
    public Vector4 position;
    public Quaternion orientation;
    public float pressure;
    public Vector3 padding;         // must be multiple of 128-bits to use shader buffers efficiently
}

public class Stroke : MonoBehaviour {

    private IBrush mBrush;

    public IBrush Brush
    {
        get { return mBrush; }
        set
        {
            if (mBrush != value)
            {
                mBrush.Dispose();
            }
            mBrush = value;
            mBrush.Refresh();
        }
    }

    public List<Vertex> Vertices;

    // Use this for initialization
    void Start () {
        Vertices = new List<Vertex>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void AddVertex(Vertex v)
    {
        Vertices.Add(v);
    }
}
