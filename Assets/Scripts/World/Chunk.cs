using UnityEngine;
using System.Collections;
using System;
using CielaSpike;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{

    public Octree<Voxel> cVoxels;
    public static int cSize = 32;

    private MeshFilter cFilter;
    private MeshCollider cColl;

    public bool cDirty = true;

    public World world;
    public Vector3i cPosition;

    public bool cGenerated = false;
    public bool cRendered = false;

    private RendererBase renderer;

    public Chunk[] SurroundingChunks = new Chunk[6];

    void Awake()
    {
        cFilter = gameObject.GetComponent<MeshFilter>();
        cColl = gameObject.GetComponent<MeshCollider>();

        renderer = new BasicWorldRenderer();
    }

    void Start()
    {
        renderer.Initialize();
    }

    void FixedUpdate()
    {
        if (cDirty)
        {
            cDirty = false;
            cRendered = false;
        }
        if (!cRendered)
            StartCoroutine(Render());
    }

    public void SetVoxel(Vector3i pos, Voxel vox)
    {
        if (InRange(pos))
        {

            cVoxels.Add(vox, pos);
            this.cDirty = true;
        }
        else
        {
            world.SetVoxel(pos, vox);
        }
    }

    public void RemoveVoxel(Vector3i pos)
    {
        if (InRange(pos))
        {
            cVoxels.Remove(pos);
            this.cDirty = true;
        }
        else
        {
            world.RemoveVoxel(pos);
        }
    }

    public Voxel GetVoxel(Vector3i pos)
    {
        if (InRange(pos))
        {
            return cVoxels.Get(pos);
        }
        return world.GetVoxel(pos);
    }

    private bool InRange(Vector3i pos)
    {
        if (pos.x < cPosition.x || pos.x >= cPosition.x + Chunk.cSize)
            return false;
        if (pos.y < cPosition.y || pos.y >= cPosition.y + Chunk.cSize)
            return false;
        if (pos.z < cPosition.z || pos.z >= cPosition.z + Chunk.cSize)
            return false;
        return true;
    }
    void Runder()
    {
        if (cGenerated)
        {
            cRendered = true;
            renderer.Render(world, this);
            Mesh tempMesh = new Mesh();
            Mesh tempCollisionMesh = new Mesh();
            tempMesh = renderer.ToMesh(tempMesh);
            tempCollisionMesh = renderer.ToCollisionMesh(tempCollisionMesh);
            cFilter.sharedMesh = tempMesh;
            cColl.sharedMesh = tempCollisionMesh;
        }
    }

    IEnumerator Render()
    {
        Task task;
        if (cGenerated)
        {
            cRendered = true;
            this.StartCoroutineAsync(ProcessMesh(), out task);
            yield return StartCoroutine(task.Wait());
            Mesh tempMesh = new Mesh();
            tempMesh = renderer.ToMesh(tempMesh);
            cFilter.sharedMesh = tempMesh;
            cColl.sharedMesh = tempMesh;
        }
        yield break;
    }

    IEnumerator ProcessMesh()
    {
        renderer.Render(world, this);
        yield break;
    }
}
