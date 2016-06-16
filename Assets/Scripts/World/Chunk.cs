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

    void Awake()
    {
        cFilter = gameObject.GetComponent<MeshFilter>();
        cColl = gameObject.GetComponent<MeshCollider>();

        renderer = new BasicWorldRenderer();
    }
    void Start()
    { 
        renderer.Initialize(world, this);
    }

    void FixedUpdate()
    {
        if (cDirty)
        {
            cDirty = false;
            cRendered = false;
        }
        if (!cRendered)
            Runder();
            //StartCoroutine(Render());
    }

    public void SetVoxel(Vector3i pos, Voxel vox, bool replace = false)
    {
        if (InRange(pos))
        {
            pos -= cPosition;
            if (replace || cVoxels[pos.x, pos.y, pos.z] == null)
            {
                cVoxels[pos.x, pos.y, pos.z] = vox;
                this.cDirty = true;
            }
        }
        else
        {
            world.SetVoxel(pos, vox, replace);
        }
    }

    public void RemoveVoxel(Vector3i pos)
    {
        if (InRange(pos))
        {
            pos -= cPosition;
            cVoxels[pos.x, pos.y, pos.z] = null;
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
        }
        return world.GetVoxel(pos);
    }

    private bool InRange(Vector3i pos)
    {
        if (pos.x < cPosition.x || pos.x >= cPosition.x + Chunk.cWidth)
            return false;
        if (pos.y < cPosition.y || pos.y >= cPosition.y + Chunk.cHeight)
            return false;
        if (pos.z < cPosition.z || pos.z >= cPosition.z + Chunk.cWidth)
            return false;
        return true;
    }

    void Runder()
    {
        if (cGenerated)
        {
            float time = Time.realtimeSinceStartup;
            cRendered = true;
            renderer.ReduceMesh();
            Mesh tempMesh = new Mesh();
            //Mesh tempColMesh = new Mesh();
            tempMesh = renderer.ToMesh(tempMesh);
            //tempColMesh = renderer.ToCollisionMesh(tempColMesh);
            cFilter.sharedMesh = tempMesh;
            //cColl.sharedMesh = tempColMesh;
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
            Mesh tempColMesh = new Mesh();
            tempMesh = renderer.ToMesh(tempMesh);
            tempColMesh = renderer.ToCollisionMesh(tempColMesh);
            cFilter.sharedMesh = tempMesh;
            cColl.sharedMesh = tempColMesh;
        }
        yield break;
    }

    IEnumerator ProcessMesh()
    {
        renderer.ReduceMesh();
        yield break;
    }
}
