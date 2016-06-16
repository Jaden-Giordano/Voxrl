using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CielaSpike;

public class BasicWorldRenderer : RendererBase
{

    private World world;
    private Chunk chunk;
    private MeshData tMeshData;
    private MeshData meshData;

    private HashSet<Vector3i> voxels;

    public void Initialize(World world, Chunk chunk)
    {
        this.world = world;
        this.chunk = chunk;
        meshData = new MeshData();
    }
    private Voxel data(int x, int y, int z)
    {
        return chunk.GetVoxel(new Vector3i(x + chunk.cPosition.x, y, z + chunk.cPosition.z));
    }

    private void GreedyMesh(int d, bool back)
    {
        int[] Axis = { Chunk.cWidth, Chunk.cHeight, Chunk.cWidth };

        int i, j, k, l, w, h, u, v, n;

        int[] x = new int[3];
        int[] q = new int[3];
        int[] du = new int[3];
        int[] dv = new int[3];

        Voxel vox = null;
        Voxel vox1 = null;

        u = (d + 1) % 3;
        v = (d + 2) % 3;

        q[d] = 1;

        Voxel[] mask = new Voxel[Axis[v] * Axis[u]];
        bool[] b = new bool[Axis[v] * Axis[u]];

        //Mask Generation
        for (x[d] = -1; x[d] < Axis[d];)
        {
            n = 0;

            for (x[v] = 0; x[v] < Axis[v]; x[v]++)
            {
                for (x[u] = 0; x[u] < Axis[u]; x[u]++)
                {
                    if (x[d] >= 0) vox = data(x[0], x[1], x[2]);
                    if (x[d] < Axis[d] - 1) vox1 = data(x[0] + q[0], x[1] + q[1], x[2] + q[2]);
                    
                    mask[n++] = ((vox != null && vox1 != null && vox.Equals(vox1))) ? null : back ? vox1 : vox;


                }
            }

            x[d]++;

            //Mesh Generation
            n = 0;

            for (j = 0; j < Axis[v]; j++)
            {
                for (i = 0; i < Axis[u];)
                {
                    if (mask[n] != null)
                    {
                        for (w = 1; i + w < Axis[u] && mask[n + w] != null && mask[n + w].Equals(mask[n]); w++) { }

                        bool done = false;
                        for (h = 1; j + h < Axis[v]; h++)
                        {
                            for (k = 0; k < w; k++)
                            {
                                if (mask[n + k + h * Axis[u]] == null || !mask[n + k + h * Axis[u]].Equals(mask[n]))
                                {
                                    done = true;
                                    break;
                                }
                            }
                            if (done) break;
                        }

                        x[u] = i;
                        x[v] = j;

                        du[u] = w;
                        dv[v] = h;

                        Vector3[] v1 = {
                                new Vector3(x[0], x[1], x[2]),
                                new Vector3(x[0] + du[0], x[1] + du[1], x[2] + du[2]),
                                new Vector3(x[0] + du[0] + dv[0], x[1] + du[1] + dv[1], x[2] + du[2] + dv[2]),
                                new Vector3(x[0] + dv[0], x[1] + dv[1], x[2] + dv[2])
                            };

                        if (back)
                            DrawFace(v1[3], v1[2], v1[1], v1[0], mask[n]);
                        else
                            DrawFace(v1[0], v1[1], v1[2], v1[3], mask[n]);


                        for (l = 0; l < h; l++)
                        {
                            for (k = 0; k < w; k++)
                            {
                                mask[n + k + l * Axis[u]] = null;
                            }
                        }
                        i += w;
                        n += w;
                    }
                    else
                    {
                        i++;
                        n++;
                    }
                }

            }
        }
    }

    public void ReduceMesh()
    {
        for (bool back = true, b = false; b != back; back = back && b, b = !b)
        {
            for (int i = 0; i < 3; i++)
            {
                GreedyMesh(i, back);
            }
        }
    }


    public Mesh ToMesh(Mesh mesh)
    {
        if (meshData.vertices.Count == 0)
        {
            GameObject.Destroy(mesh);
            return null;
        }

        if (mesh == null)
            mesh = new Mesh();

        mesh.vertices = meshData.VertexArray();
        mesh.colors32 = meshData.ColorArray();
        mesh.triangles = meshData.TriangleArray();

        mesh.RecalculateNormals();

        return mesh;
    }

    public Mesh ToCollisionMesh(Mesh mesh)
    {
        if (meshData.colVertices.Count == 0)
        {
            GameObject.Destroy(mesh);
            return null;
        }

        if (mesh == null)
            mesh = new Mesh();

        mesh.vertices = meshData.ColVertexArray();
        mesh.triangles = meshData.ColTriangleArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        return mesh;
    }

    private void DrawFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Voxel vox)
    {
        v1 += chunk.cPosition.ToVector3();
        v2 += chunk.cPosition.ToVector3();
        v3 += chunk.cPosition.ToVector3();
        v4 += chunk.cPosition.ToVector3();

        v1 *= Voxel.vSize;
        v2 *= Voxel.vSize;
        v3 *= Voxel.vSize;
        v4 *= Voxel.vSize;

        int index = meshData.vertices.Count;

        meshData.AddVertex(v1);
        meshData.AddVertex(v2);
        meshData.AddVertex(v3);
        meshData.AddVertex(v4);

        meshData.AddTriangle(index);
        meshData.AddTriangle(index + 1);
        meshData.AddTriangle(index + 2);

        meshData.AddTriangle(index);
        meshData.AddTriangle(index + 2);
        meshData.AddTriangle(index + 3);

        meshData.AddColor(vox.vColor);

        if (vox.vColor.a > 0.9)
        {
            int cIndex = meshData.colVertices.Count;
            meshData.AddColVertex(v1);
            meshData.AddColVertex(v2);
            meshData.AddColVertex(v3);
            meshData.AddColVertex(v4);

            meshData.AddColTriangle(cIndex);
            meshData.AddColTriangle(cIndex + 1);
            meshData.AddColTriangle(cIndex + 2);

            meshData.AddColTriangle(cIndex);
            meshData.AddColTriangle(cIndex + 2);
            meshData.AddColTriangle(cIndex + 3);
        }

    }
}