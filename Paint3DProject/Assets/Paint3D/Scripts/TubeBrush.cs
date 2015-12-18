using System;
using System.Collections.Generic;
using UnityEngine;

public class TubeBrush : Brush
{
    GameObject go;
    int last;
    Mesh mesh;

    int sides = 6;

    public override string BrushName { get { return "TubeBrush"; } }

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
        throw new NotImplementedException();
    }

    public override void Refresh()
    {
        
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        throw new NotImplementedException();
    }

    //public TubeBrush(Stroke stroke)
    //    : base(stroke)
    //{
    //    go = new GameObject("Tube");
    //    mesh = (go.AddComponent<MeshFilter>()).mesh;
    //    //go.AddComponent<MeshRenderer>();
        
    //}
}