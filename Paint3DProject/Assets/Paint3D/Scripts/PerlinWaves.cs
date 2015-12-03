/*
* copyright 2014 Daniel Fairley (poemdexter@gmail.com)
*/ 

using UnityEngine;

public class PerlinWaves : MonoBehaviour
{
    public float perlinPower;   // power of perlin noise
    public float sinePower;     // power of sine wave noise
    private float currentTime;

    private void Update()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] baseHeight = mesh.vertices;
        Vector3[] vertices = new Vector3[baseHeight.Length];

        currentTime += Time.deltaTime;

        // apply the noise to each vertex on the mesh
        for (var i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            vertex.y = sinePower * Mathf.Sin(currentTime + (vertex.z * 1)) + sinePower * Mathf.Sin(currentTime + (vertex.x * 1));
            vertex.y += Mathf.PerlinNoise(vertex.x + currentTime, vertex.z + currentTime - 0.5f) * perlinPower;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}