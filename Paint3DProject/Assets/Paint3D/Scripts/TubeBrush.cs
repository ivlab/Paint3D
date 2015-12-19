using System;
using System.Collections.Generic;
using UnityEngine;

public class TubeBrush : Brush
{
    private int   SideCount = 6;
    private Color StartColor = Color.gray;
    private Color EndColor = Color.green;
    private float StartWidth = 0.25f;
    private float EndWidth = 0.125f;

    List<Vector3> verts;
    List<int> inds;
    List<Color> colors;
    
    int last;
    MeshFilter mf;
    MeshRenderer mr;
    Mesh mesh;

    public override string BrushName { get { return "TubeBrush"; } }
    public override void AddVertex(Vertex v)
    {
        //if (ver)
        //{
        //
        //}
        //List<Vector3> verts =new List<Vector3>( mesh.vertices);
        //Vertex prev = Stroke.Vertices[last];
        Refresh();
    }

    public override void Dispose()
    {
        mesh = null;
        Destroy(mf);
        Destroy(mr);
    }

    public override void Update()
    {
        //throw new NotImplementedException();
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
        if (mStroke == null)
        {
            return;
        }
        if (mStroke.Vertices.Count == 0)
        {
            return;
        }

        // clear old data
        verts.Clear();
        inds.Clear();
        colors.Clear();

        float theta = 360.0f / SideCount;

        Vector3[] poly = new Vector3[SideCount];

        Vector3 rad = new Vector3(StartWidth, 0, 0);
        Quaternion polyrot = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        for (int j = 0; j < SideCount; j++)
        {
            Quaternion q = Quaternion.Euler(0,0,theta * j);
            poly[j] = polyrot * q *  rad;
            //Debug.Log(poly[j]);
        }
       
        int count = Stroke.Vertices.Count;

        // first cross-section
        Vector3 center = Stroke.Vertices[0].position;
        Quaternion rot = Stroke.Vertices[0].orientation;
        for (int j = 0; j < SideCount; j++)
        {
            Vector3 v = rot * poly[j];
            v += center;
            verts.Add(v);
            colors.Add(StartColor);
            //inds.Add((j + SideCount) % SideCount);
        }

        for (int i = 1; i < count; i++)
        {
            Vector3 c = Stroke.Vertices[i].position;
            Quaternion q = Stroke.Vertices[i].orientation;
            int n = i * SideCount;

            float span = (float)i / (float)count;
            Color color = Color.Lerp(StartColor, EndColor, span);

            for (int j = 0; j < SideCount; j++)
            {
                Vector3 v = q * poly[j];
                v += c;
                verts.Add(v);
                colors.Add(color);

                // tri 1
                inds.Add(n + j);
                inds.Add(j + n - SideCount);
                inds.Add((j + 1) % SideCount + n);

                // tri 2
                inds.Add((j + 1) % SideCount + n);
                inds.Add(n + j - SideCount);
                inds.Add(n + (j + 1) % SideCount - SideCount);

                //inds.Add(n + j);
                //inds.Add(n + j - SideCount);
                //
                //inds.Add(n + j - SideCount);
                //inds.Add((j + 1) % SideCount + n);
                //
                //inds.Add((j + 1) % SideCount + n);
                //inds.Add(n + j);

                //inds.Add((j + 1) % SideCount + n);
                //inds.Add(n + j - SideCount);
                //
                //inds.Add(n + j - SideCount);
                //inds.Add(n + (j + 1) % SideCount - SideCount);
                //
                //inds.Add(n + (j + 1) % SideCount - SideCount);
                //inds.Add((j + 1) % SideCount + n);
            }
        }

        mesh.SetVertices(verts);
        mesh.SetColors(colors);
        mesh.SetIndices(inds.ToArray(), MeshTopology.Triangles, 0);
        //Vector3[] pos = mesh.vertices;
        
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
            
            mf = mStroke.gameObject.AddComponent<MeshFilter>();
            mr = mStroke.gameObject.AddComponent<MeshRenderer>();
            mesh = mf.mesh;
            Material tubeMat = new Material(Shader.Find("Sprites/Default"));
            mr.material = tubeMat;
            mesh.subMeshCount = 1;
            
        }
    }

    public TubeBrush()
    {
        verts = new List<Vector3>();
        inds = new List<int>();
        colors = new List<Color>();
    }
}