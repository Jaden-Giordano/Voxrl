using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicWorldRenderer : RendererBase
{

    private World world;
    private Chunk chunk;
    private MeshData meshData;

    private HashSet<Vector3i> voxels;

    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Color32> colors = new List<Color32>();


    public void Initialize()
    {
        meshData = new MeshData();
    }

    public void Render(World world, Chunk chunk)
    {
        this.world = world;
        this.chunk = chunk;

        voxels = new HashSet<Vector3i>(chunk.cVoxels.GetPositions());

        foreach (Vector3i vox in voxels)
        {
            Voxel voxel = chunk.GetVoxel(vox);

            Vector3 start = new Vector3(vox.x, vox.y, vox.z);
            if (MustFaceBeVisible(vox.x - 1, vox.y, vox.z))
            {
                DrawFace((start) * Voxel.vSize, (start + Vector3.forward) * Voxel.vSize, (start + Vector3.forward + Vector3.up) * Voxel.vSize, (start + Vector3.up) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x + 1, vox.y, vox.z))
            {
                DrawFace((start + Vector3.right + Vector3.forward) * Voxel.vSize, (start + Vector3.right) * Voxel.vSize, (start + Vector3.right + Vector3.up) * Voxel.vSize, (start + Vector3.right + Vector3.forward + Vector3.up) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y - 1, vox.z))
            {
                DrawFace((start + Vector3.forward) * Voxel.vSize, (start) * Voxel.vSize, (start + Vector3.right) * Voxel.vSize, (start + Vector3.right + Vector3.forward) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y + 1, vox.z))
            {
                DrawFace((start + Vector3.up) * Voxel.vSize, (start + Vector3.up + Vector3.forward) * Voxel.vSize, (start + Vector3.up + Vector3.forward + Vector3.right) * Voxel.vSize, (start + Vector3.up + Vector3.right) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y, vox.z - 1))
            {
                DrawFace((start + Vector3.right) * Voxel.vSize, (start) * Voxel.vSize, (start + Vector3.up) * Voxel.vSize, (start + Vector3.up + Vector3.right) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y, vox.z + 1))
            {
                DrawFace((start + Vector3.forward) * Voxel.vSize, (start + Vector3.forward + Vector3.right) * Voxel.vSize, (start + Vector3.forward + Vector3.right + Vector3.up) * Voxel.vSize, (start + Vector3.forward + Vector3.up) * Voxel.vSize, voxel);
            }
        }
    }

    public Mesh ToMesh(Mesh mesh)
    {
        if (vertices.Count == 0)
        {
            GameObject.Destroy(mesh);
            return null;
        }

        if (mesh == null)
            mesh = new Mesh();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors32 = colors.ToArray();

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();

        Logger.Instance.OutputLog();

        return mesh;
    }

    private void DrawFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Voxel voxel)
    {
        int index = vertices.Count;
            vertices.Add(v1);
            vertices.Add(v2);
            vertices.Add(v3);
            vertices.Add(v4);

            triangles.Add(index);
            triangles.Add(index + 1);
            triangles.Add(index + 2);

            triangles.Add(index);
            triangles.Add(index + 2);
            triangles.Add(index + 3);

            colors.Add(voxel.vColor);
            colors.Add(voxel.vColor);
            colors.Add(voxel.vColor);
            colors.Add(voxel.vColor);
    }

    private bool MustFaceBeVisible(int x, int y, int z)
    {
        Vector3i pos = new Vector3i(x, y, z);

        if (chunk.GetVoxel(pos) != null)
        {
            if (chunk.GetVoxel(pos).vColor.a == 1)
            {
                return true;
            }else
            {
                return false;
            }
        }
        return true;
    }
}