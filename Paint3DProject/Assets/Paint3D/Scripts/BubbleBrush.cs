using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleBrush : Brush
{
    private Color BubbleColor = Color.cyan;
	private float minSize = 0.1f;
	private float maxSize = 0.25f;
	private float rate = 5f;
	private float density = 10.0f;

    GameObject go;
    ParticleSystem ps;
    Mesh emitMesh;

    public override string BrushName { get { return "BubbleBrush"; } }

    public override void AddVertex(Vertex v)
    {
        // 
    }

    public override void Dispose()
    {
        Destroy(go);
    }

    public override void Update()
    {
        float dt = Time.deltaTime;
        int length = Stroke.Vertices.Count;
        //EmitParams emitParams = new EmitParams();
        //emitParams.startLifetime = 1f;
        //emitParams.startSize = 0.1f;

        float life = 1f;
        float size = 0.1f;

        Vector3 vel;
        Vector3 pos;

        
        for (int i = 0; i < length; i++)
        {
            float r = Random.value;
            float probability = rate * dt;
            int count = (int)probability;
            probability = probability - (float)Math.Truncate(probability);
            //emitParams.velocity = Random.onUnitSphere;
            //emitParams.position = (Vector3)(mStroke.Vertices[i].position) + Random.insideUnitSphere / density;
            vel = Random.onUnitSphere;
            pos = (Vector3)(mStroke.Vertices[i].position) + Random.insideUnitSphere / density;
            if (r < probability)
                count++;

            //ps.Emit(emitParams, count);
            for (int j = 0; j < count; i++)
            {
                ps.Emit(pos, vel, size, life, Color.black);
            }
        }
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
        //throw new NotImplementedException();
    }

    public override void SetOptions(Dictionary<string, object> newOptions)
    {
        //
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
            emitMesh = new Mesh();
            go = Instantiate(Resources.Load<GameObject>("Prefabs/BubbleEmiter"));
            go.transform.parent = mStroke.gameObject.transform;
            ps = go.GetComponent<ParticleSystem>();
            ps.Stop();
        }
    }
}
