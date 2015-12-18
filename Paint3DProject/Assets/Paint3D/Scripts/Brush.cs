using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Brush : ScriptableObject, IBrush
{
    public Stroke Stroke { get; protected set; }
    public abstract void AddVertex(Vertex v);
    public abstract void Dispose();
    public abstract void Draw();
    public abstract Dictionary<string, object> GetOptions();
    public abstract void Refresh();
    public abstract void SetOptions(Dictionary<string, object> newOptions);

    public Brush(Stroke stroke)
    {
        Stroke = stroke;
    }
}
    
