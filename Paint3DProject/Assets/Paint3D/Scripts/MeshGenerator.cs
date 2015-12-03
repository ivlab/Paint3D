/*
* copyright 2014 Daniel Fairley (poemdexter@gmail.com)
*/ 

using System;
using UnityEngine;

#if !UNITY_WEBPLAYER
using UnityEditor;
#endif

public class MeshGenerator : MonoBehaviour
{
    public int dimension;       // squared dimension of the generated mesh
    public bool isTerrain;      // flag for adding noise to mesh during generation
    public float terrainPower;  // power of noise added to mesh during generation
    public bool saveMesh;       // flag for saving mesh after generation

    public void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        // create square set of points
        Vector3[] points = new Vector3[(int)Math.Pow(dimension + 1, 2)];

        // fill set with points
        int j = 0;
        for (int x = 0; x <= dimension; x++)
        {
            for (int z = 0; z <= dimension; z++)
            {
                points[j] = new Vector3(x, 0, z);
                j++;
            }
        }

        // clear the mesh
        Mesh mesh = new Mesh();

        // create set of verts
        Vector3[] verts = new Vector3[6 * (int)Math.Pow(dimension, 2)];

        // fill set with verts from points
        int k = 0;
        for (int x = 0; x < dimension; x++)
        {
            for (int z = 0; z < dimension; z++)
            {
                // top triangle of quad
                verts[k] = points[z + ((x * dimension) + x) + 0];
                k++;
                verts[k] = points[z + ((x * dimension) + x) + 1];
                k++;
                verts[k] = points[z + ((x * dimension) + x) + dimension + 2];
                k++;

                // bottom triangle of quad
                verts[k] = points[z + ((x * dimension) + x) + 0];
                k++;
                verts[k] = points[z + ((x * dimension) + x) + dimension + 2];
                k++;
                verts[k] = points[z + ((x * dimension) + x) + dimension + 1];
                k++;
            }
        }

        // create set of triangles
        int[] tris = new int[6 * (int)Math.Pow(dimension, 2)];
        for (int i = 0; i < tris.Length; i++)
        {
            tris[i] = i;
        }

        // create set of UVs
        Vector2[] uvs = new Vector2[verts.Length];
        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = new Vector2(verts[i].x, verts[i].z);
        }

        // assign everything to mesh
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        meshFilter.mesh = mesh;

        // check if terrain and add noise if needed
        if (isTerrain)
        {
            MakeTerrain();
        }

#if !UNITY_WEBPLAYER
        // check if we're saving this mesh
        if (saveMesh)
        {
            AssetDatabase.CreateAsset(mesh, "Assets/Models/generatedMesh.prefab");
            AssetDatabase.SaveAssets();
        }
#endif
    }

    /// <summary>
    /// Builds the terrain by raising vertices at each point up a random amount determined by perlin noise
    /// </summary>
    private void MakeTerrain()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] baseHeight = mesh.vertices;
        Vector3[] vertices = new Vector3[baseHeight.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            if (vertex.x != 0 && vertex.z != 0 && vertex.x != dimension && vertex.z != dimension) // if not an edge point
            {
                vertex.y = Mathf.PerlinNoise(vertex.x + 0.3f, vertex.z + 0.1f) * terrainPower;
            }
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}