using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData {

    //Value Key
    public Dictionary<Vector3, int> vertices = new Dictionary<Vector3, int>();
    public Dictionary<Vector3, int> colVertices = new Dictionary<Vector3, int>();
    //Key Value
    public Dictionary<int, int> triangles = new Dictionary<int, int>();
    public Dictionary<int, Color32> colors = new Dictionary<int, Color32>();
    public Dictionary<int, int> colTriangles = new Dictionary<int, int>();

    public bool useRenderDataForCol = false;

    public MeshData() { }

    public void Clear()
    {
        vertices.Clear();
        triangles.Clear();
        colors.Clear();
        colVertices.Clear();
        colTriangles.Clear();
    }

    public void AddVertex(Vector3 vertex)
    {
        vertices.Add(vertex, vertices.Count);

        if (useRenderDataForCol)
        {
            colVertices.Add(vertex, colVertices.Count);
        }
    }

    public void AddTriangle(int tri)
    {
        triangles.Add(triangles.Count, tri);

        if (useRenderDataForCol)
        {
            colTriangles.Add(colTriangles.Count, tri - (vertices.Count - colVertices.Count));
        }
    }

    public void AddColor(Color32 color)
    {
        colors.Add(colors.Count, color);
        colors.Add(colors.Count, color);
        colors.Add(colors.Count, color);
        colors.Add(colors.Count, color);
    }

    public Vector3[] VertexArray()
    {
        Vector3[] tempArray = new Vector3[vertices.Count];
        int i = 0;
        foreach(Vector3 key in vertices.Keys)
        {
            tempArray[i] = key;
            i++;
        }
        return tempArray;
    }

    public int[] TriangleArray()
    {
        int[] tempArray = new int[triangles.Count];
        int i = 0;
        foreach (int value in triangles.Values)
        {
            tempArray[i] = value;
            i++;
        }
        return tempArray;
    }

    public Color32[] ColorArray()
    {
        Color32[] tempArray = new Color32[colors.Count];
        int i = 0;
        foreach (Color32 value in colors.Values)
        {
            tempArray[i] = value;
            i++;
        }
        return tempArray;
    }
}