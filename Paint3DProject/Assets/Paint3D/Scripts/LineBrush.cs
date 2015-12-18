using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LineBrush : Brush {

    private int count;
    private Stroke stroke;
    private LineRenderer lr;

    // options
    private float startWidth;
    private float endWidth;
    private Color startColor;

    public override string BrushName
    {
        get
        {
            return "BrushName";
        }
    }

    public override void AddVertex(Vertex v)
    {
        lr.SetPosition(count, v.position);
    }

    public override void Dispose()
    {
        UnityEngine.Object.Destroy(lr);
    }

    public override void Draw()
    {
        //
    }

    public override void Refresh()
    {
        int length = stroke.Vertices.Count;
        
        lr.SetVertexCount(length);

        for (int i = 0; i < length; i++)
        {
            lr.SetPosition(i, stroke.Vertices[i].position);
        }
    }

    public override Dictionary<string, object> GetOptions()
    {
        Dictionary<string, object> opt = new Dictionary<string, object>();
        opt.Add("StartWidth", startWidth);
        opt.Add("EndWidth", endWidth);
        opt.Add("StartColor", startColor);
        return opt;
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        object startW, endW, startC, endC;
        if (newOptions.TryGetValue("StartWidth", out startW))
        {
            if (startW is float)
            {
                startWidth = (float)startW;
            }
        }
    }

    public LineBrush(Stroke s)
    {
        stroke = s;
        lr = stroke.gameObject.AddComponent<LineRenderer>();
    }
}
