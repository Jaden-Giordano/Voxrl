using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeshData {

    //Value Key
    public Dictionary<int, Vector3> vertices = new Dictionary<int, Vector3>();
    public Dictionary<int, Vector3> colVertices = new Dictionary<int, Vector3>();
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
        vertices.Add(vertices.Count, vertex);

        if (useRenderDataForCol)
        {
            colVertices.Add(vertices.Count, vertex);
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
        foreach(Vector3 value in vertices.Values)
        {
            tempArray[i] = value;
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