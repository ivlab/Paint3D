using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBrush : Brush
{
    private Color BubbleColor;
	private float minSize;
	private float maxRadius;
	private float rate;
	private float density;

    GameObject go;

    public override string BrushName { get { return "BubbleBrush"; } }

    public override void AddVertex(Vertex v)
    {
        //
    }

    public override void Dispose()
    {
        Destroy(go);
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
        throw new NotImplementedException();
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        throw new NotImplementedException();
    }

    //public BubbleBrush(Stroke stroke)
    //    : base(stroke)
    //{
    //    go = new GameObject("Bubbles");
    //}
}
