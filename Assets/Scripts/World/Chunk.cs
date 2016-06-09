using UnityEngine;
using System.Collections;
using System;
using CielaSpike;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour
{

    public Voxel[,,] cVoxels;

    //public Octree<Voxel> cVoxels;
    public static int cWidth = 32;
    public static int cHeight = 12*32;

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

    void FixedUpdate()
    {
        if (cDirty)
        {
            cDirty = false;
            cRendered = false;
        }
        if (!cRendered)
            StartCoroutine(Render());
            //Runder();

        //MeshData errors - Duplicate vertex indexes, not sure how that's happening, possibly due to the multithread thing
        //Without Multithread - Only renders a 32x32x32 instead of a whole chunk (32x384x32)
    }

    public void SetVoxel(Vector3i pos, Voxel vox)
    {
        if (InRange(pos))
        {
            pos -= cPosition;
            cVoxels[pos.x, pos.y, pos.z] = vox;
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
            pos -= cPosition;
            cVoxels[pos.x, pos.y, pos.z] = null;
            //cVoxels.Remove(pos);
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
            pos -= cPosition;
            return cVoxels[pos.x, pos.y, pos.z];
            //return cVoxels.Get(pos);
        }
        return world.GetVoxel(pos);
    }

    private bool InRange(Vector3i pos)
    {
        if (pos.x < cPosition.x || pos.x >= cPosition.x + Chunk.cWidth)
            return false;
        if (pos.z < cPosition.z || pos.z >= cPosition.z + Chunk.cWidth)
            return false;
        return true;
    }
    void Runder()
    {
        if (cGenerated)
        {
            renderer.Initialize(world, this);
            cRendered = true;
            Mesh tempMesh = new Mesh();
            renderer.ReduceMesh();
            tempMesh = renderer.ToMesh(tempMesh);
            cFilter.sharedMesh = tempMesh;
            cColl.sharedMesh = tempMesh;
        }
    }

    IEnumerator Render()
    {
        Task task;
        if (cGenerated)
        {
            renderer.Initialize(world, this);
            cRendered = true;
            this.StartCoroutineAsync(ProcessMesh(), out task);
            yield return StartCoroutine(task.Wait());
            Mesh tempMesh = new Mesh();
            //Mesh tempColMesh = new Mesh();
            tempMesh = renderer.ToMesh(tempMesh);
            //tempColMesh = renderer.ToCollisionMesh(tempColMesh);
            cFilter.sharedMesh = tempMesh;
            //cColl.sharedMesh = tempColMesh;
        }
        yield break;
    }

    IEnumerator ProcessMesh()
    {
        renderer.ReduceMesh();
        yield break;
    }
}
