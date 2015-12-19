using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class LineBrush : Brush {

    private int count;
    private LineRenderer lr;

    // options
    private float startWidth = 1.0f;
    private float endWidth = 0.0f;
    private Color startColor = Color.blue;
    private Color endColor = Color.gray;

    public override string BrushName
    {
        get
        {
            return "LineBrush";
        }
    }

    public override void AddVertex(Vertex v)
    {
        lr.SetVertexCount(count + 1);
        lr.SetPosition(count, v.position);
        count++;
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
        if (mStroke.enabled)
        {

            int length = mStroke.Vertices.Count;

            lr.SetVertexCount(length);

            for (int i = 0; i < length; i++)
            {
                lr.SetPosition(i, mStroke.Vertices[i].position);
            }
        }
    }

    public override Dictionary<string, object> GetOptions()
    {
        Dictionary<string, object> opt = new Dictionary<string, object>();
        opt.Add("StartWidth", startWidth);
        opt.Add("EndWidth", endWidth);
        opt.Add("StartColor", startColor);
        opt.Add("EndColor", endColor);
        return opt;
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        object startW; // = startWidth;
        object endW;// = endWidth;
        object startC;// = startColor;
        object endC;// = endColor;

        if (newOptions.TryGetValue("StartWidth", out startW))
        {
            if (startW is float)
            {
                startWidth = (float)startW;
            }
        }
        if (newOptions.TryGetValue("EndWidth", out endW))
        {
            if (endW is float)
            {
                endWidth = (float)endW;
            }
        }
        if (newOptions.TryGetValue("StartWidth", out startC))
        {
            if (startW is Color)
            {
                startColor = (Color)startC;
            }
        }
        if (newOptions.TryGetValue("EndColor", out endC))
        {
            if (endW is Color)
            {
                endColor = (Color)endC;
            }
        }
    }

    public override Stroke Stroke
    {
        get
        {
            return base.Stroke;
        }

        set
        {
            base.Stroke = value;
            lr = mStroke.gameObject.AddComponent<LineRenderer>();
            lr.SetWidth(startWidth, endWidth);
            lr.SetColors(startColor, endColor);
            Material lineMat = new Material(Shader.Find("Sprites/Default"));
            lr.material = lineMat;

        }
    }
}
