using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBrush : Brush
{
    private Color BubbleColor = Color.cyan;
	private float minSize = 0.4f;
	private float maxSize = 2.0f;
	private float rate = 1.0f;
	private float density = 1.0f;

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
        //
    }

    public override Dictionary<string, object> GetOptions()
    {
        Dictionary<string, object> opt = new Dictionary<string, object>();
        opt.Add("Color", BubbleColor);
        opt.Add("minSize", minSize);
        opt.Add("maxSize", maxSize);
        opt.Add("rate", rate);
        opt.Add("density", density);
        return opt;
    }

    public override void Refresh()
    {
        throw new NotImplementedException();
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        throw new NotImplementedException();
    }
}
