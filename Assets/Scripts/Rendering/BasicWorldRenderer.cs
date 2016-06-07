using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicWorldRenderer : RendererBase
{

    private World world;
    private Chunk chunk;
    private MeshData meshData;

    private HashSet<Vector3i> voxels;

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
            if (MustFaceBeVisible(vox.x - 1, vox.y, vox.z, voxel.vColor.a))
            {
                DrawFace((start) * Voxel.vSize, (start + Vector3.forward) * Voxel.vSize, (start + Vector3.forward + Vector3.up) * Voxel.vSize, (start + Vector3.up) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x + 1, vox.y, vox.z, voxel.vColor.a))
            {
                DrawFace((start + Vector3.right + Vector3.forward) * Voxel.vSize, (start + Vector3.right) * Voxel.vSize, (start + Vector3.right + Vector3.up) * Voxel.vSize, (start + Vector3.right + Vector3.forward + Vector3.up) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y - 1, vox.z, voxel.vColor.a))
            {
                DrawFace((start + Vector3.forward) * Voxel.vSize, (start) * Voxel.vSize, (start + Vector3.right) * Voxel.vSize, (start + Vector3.right + Vector3.forward) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y + 1, vox.z, voxel.vColor.a))
            {
                DrawFace((start + Vector3.up) * Voxel.vSize, (start + Vector3.up + Vector3.forward) * Voxel.vSize, (start + Vector3.up + Vector3.forward + Vector3.right) * Voxel.vSize, (start + Vector3.up + Vector3.right) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y, vox.z - 1, voxel.vColor.a))
            {
                DrawFace((start + Vector3.right) * Voxel.vSize, (start) * Voxel.vSize, (start + Vector3.up) * Voxel.vSize, (start + Vector3.up + Vector3.right) * Voxel.vSize, voxel);
            }
            if (MustFaceBeVisible(vox.x, vox.y, vox.z + 1, voxel.vColor.a))
            {
                DrawFace((start + Vector3.forward) * Voxel.vSize, (start + Vector3.forward + Vector3.right) * Voxel.vSize, (start + Vector3.forward + Vector3.right + Vector3.up) * Voxel.vSize, (start + Vector3.forward + Vector3.up) * Voxel.vSize, voxel);
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
        mesh.triangles = meshData.TriangleArray();
        mesh.colors32 = meshData.ColorArray();

        mesh.RecalculateNormals();
        mesh.Optimize();

        return mesh;
    }

    private void DrawFace(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4, Voxel voxel)
    {
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

        meshData.AddColor(voxel.vColor);
    }

    private bool MustFaceBeVisible(int x, int y, int z, float a)
    {
        Vector3i pos = new Vector3i(x, y, z);
        if (chunk.GetVoxel(pos) == null)
            return true;
        /*if (a < 1 && chunk.GetVoxel(pos).vColor.a < 1)
            return false;*/
        if (a == 255 && chunk.GetVoxel(pos).vColor.a < 255)
            return true;
        return false;
    }
}