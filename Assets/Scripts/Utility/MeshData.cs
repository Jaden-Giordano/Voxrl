using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData
{
    public HashArray<Vector3> vertices = new HashArray<Vector3>();
    public HashArray<Vector3> colVertices = new HashArray<Vector3>();
    public HashArray<int> triangles = new HashArray<int>();
    public HashArray<int> colTriangles = new HashArray<int>();
    public HashArray<Color32> colors = new HashArray<Color32>();

    public MeshData() { }

    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex);
    }
    
    public void AddColVertex(Vector3 vertex)
    {
        colVertices.Add(vertex);
    }

    public void AddTriangle(int tri)
    {
        triangles.Add(tri);
    }
    public void AddColTriangle(int tri)
    {
        colTriangles.Add(tri);
    }

    public void AddColor(Color32 color)
    {
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
        colors.Add(color);
    }

    public Vector3[] VertexArray()
    {
        return vertices.ToArray();
    }

    public int[] TriangleArray()
    {
        return triangles.ToArray();
    }

    public Vector3[] ColVertexArray()
    {
        return colVertices.ToArray();
    }

    public int[] ColTriangleArray()
    {
        return colTriangles.ToArray();
    }

    public Color32[] ColorArray()
    {
        return colors.ToArray();
    }
}