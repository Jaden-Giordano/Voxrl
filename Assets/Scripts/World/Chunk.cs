using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]

public class Chunk : MonoBehaviour {

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

	void Awake() {
        Debug.Log(Vector3i.zero.ToString());
        cVoxels = new Octree<Voxel>(cSize, new Vector3i(cPosition.x + cSize / 2, cPosition.y + cSize / 2, cPosition.z + cSize / 2), 8);

		cFilter = gameObject.GetComponent<MeshFilter> ();
		cColl = gameObject.GetComponent<MeshCollider> ();

		renderer = new RendererBase ();
		renderer.Initialize();
	}

	void FixedUpdate() {
		if (cDirty) {
			cDirty = false;
			cRendered = false;
		}
		if (!cRendered)
			Render ();
	}

    public void SetVoxel(Vector3i pos, Color32 color)
    {
        if (InRange(pos))
        {
            Voxel tempVoxel = new Voxel();
            tempVoxel.SetColor(color);
            cVoxels.Add(tempVoxel, pos);
            this.cDirty = true;
        }
        else
        {
            world.SetVoxel(pos, color);
        }
    }

    public void RemoveVoxel(Vector3i pos)
    {
        if(InRange(pos))
        {
            cVoxels.Remove(pos);
            this.cDirty = true;
        }else
        {
            world.RemoveVoxel(pos);
        }
    }

	public Voxel GetVoxel(Vector3i pos) {
        if (InRange(pos))
            return cVoxels.Get(pos);
		return world.GetVoxel (pos);
	}

    private bool InRange(Vector3i pos)
    {
        /*if (pos.x < cPosition.x || pos.x >= cPosition.x + Chunk.cSize)
            return false;
        if (pos.y < cPosition.y || pos.y >= cPosition.y + Chunk.cSize)
            return false;
        if (pos.z < cPosition.z || pos.z >= cPosition.z + Chunk.cSize)
            return false;*/
        return true;
    }

	void Render() {
		if (cGenerated) {
			cRendered = true;
			renderer.Render (world, this);
			Mesh tempMesh = new Mesh ();
			tempMesh = renderer.ToMesh (tempMesh);
			cFilter.sharedMesh = tempMesh;
			cColl.sharedMesh = tempMesh;
		}
	}
}
