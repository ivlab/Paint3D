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
    private Material material;

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
        opt.Add("Start Width", startWidth);
        opt.Add("End Width", endWidth);
        opt.Add("Material", material);
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        object start, end, mat;
        if (newOptions.TryGetValue("Start Width", out start))
        {
            if (start is float)
            {
                startWidth = start as float;
            }
        }
    }

    public LineBrush(Stroke s)
    {
        stroke = s;
        lr = stroke.gameObject.AddComponent<LineRenderer>();
    }
}
