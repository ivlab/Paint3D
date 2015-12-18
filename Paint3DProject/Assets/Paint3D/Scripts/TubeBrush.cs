using System;
using System.Collections.Generic;
using UnityEngine;

public class TubeBrush : Brush
{
    private int   SideCount;
    private Color StartColor;
    private Color EndColor;
    private float StartWidth;
    private float EndWidth;

    GameObject go;
    int last;
    Mesh mesh;

    int sides = 6;

    public override string BrushName
    {
        get
        {
            return "TubeBrush";
        }
    }

    public override void AddVertex(Vertex v)
    {
        List<Vector3> verts =new List<Vector3>( mesh.vertices);
        Vertex prev = Stroke.Vertices[last];
        
    }

    public override void Dispose()
    {
        throw new NotImplementedException();
    }

    public override void Draw()
    {
        throw new NotImplementedException();
    }

    public override Dictionary<string, object> GetOptions()
    {
        Dictionary<string, object> opt = new Dictionary<string, object>();
        opt.Add("SideCount", SideCount);
        opt.Add("StartColor", StartColor);
        opt.Add("EndColor", EndColor);
        opt.Add("StartWidth", StartWidth);
        opt.Add("EndWidth", EndWidth);
        return opt;
    }

    public override void Refresh()
    {
        
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        throw new NotImplementedException();
    }
}