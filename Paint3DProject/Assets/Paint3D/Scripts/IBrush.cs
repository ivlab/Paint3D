using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IBrush
{
    void AddVertex(Vertex v);

    void SetOptions(Dictionary<string, object> newOptions);

    Dictionary<string, object> GetOptions();

    Stroke Stroke { get; set; }
    string BrushName { get; }

    /// <summary>
    /// Refreshes the Brush
    /// </summary>
    void Refresh();

    /// <summary>
    /// Called every Update
    /// </summary>
    void Update();

    /// <summary>
    /// Called when removing brush from stroke
    /// </summary>
    void Dispose();
}
