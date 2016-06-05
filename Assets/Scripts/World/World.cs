using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class World : MonoBehaviour
{

    public Octree<Chunk> wChunks;

    public int wSize;
    public int wMinSize = 256;

    public Vector3i cGenerationPos;

    private Queue<Vector3i> cToGenerate = new Queue<Vector3i>();

    GeneratorBase cGenerator;
    public bool cGenerate = true;

    public GameObject chunkPrefab;

    public Vector3 worldSize;

    void Awake()
    {
        wChunks = new Octree<Chunk>(wSize, Vector3.zero, wMinSize);

        cGenerator = new BasicWorldGeneration();
    }

    void FixedUpdate()
    {
        if (cGenerate)
        {
            for (int x = -(int)worldSize.x; x < worldSize.x; x++) 
            {
                for (int y = -(int)worldSize.y; y < worldSize.y; y++)
                {
                    for (int z = -(int)worldSize.z; z < worldSize.z; z++)
                    {
                        AddChunk(new Vector3i(x, y, z));
                    }
                }
            }
        }

        StartCoroutine(CreateChunk());
    }

    //Keep this function here, It doesn't work in it's old position below RemoveVoxel
    //No Idea Why (Fecking Microsoft Sabotage)
    public Voxel GetVoxel(Vector3i pos)
    {
        Chunk tempChunk = GetChunk(pos);
        if (tempChunk != null)
            return tempChunk.GetVoxel(pos);
        return null;
    }

    public void SetVoxel(Vector3i pos, Voxel vox)
    {
        Chunk tempChunk = GetChunk(pos);
        if (tempChunk != null)
            tempChunk.SetVoxel(pos, vox);
    }

    public void RemoveVoxel(Vector3i pos)
    {
        Chunk tempChunk = GetChunk(pos);
        if (tempChunk != null)
            tempChunk.RemoveVoxel(pos);
    }

    public Chunk GetChunk(Vector3i pos)
    {
        pos.x = Mathf.FloorToInt(pos.x / (float)Chunk.cSize) * Chunk.cSize;
        pos.y = Mathf.FloorToInt(pos.y / (float)Chunk.cSize) * Chunk.cSize;
        pos.z = Mathf.FloorToInt(pos.z / (float)Chunk.cSize) * Chunk.cSize;

        return wChunks.Get(pos);
    }

    public void AddChunk(Vector3i pos)
    {
        cGenerate = false;
        cToGenerate.Enqueue(pos);
    }

    public void DestroyChunk(Vector3i pos)
    {
        wChunks.Remove(pos);
    }

    IEnumerator CreateChunk()
    {
        Vector3i[] TempPositions = new Vector3i[cToGenerate.Count];
        cToGenerate.CopyTo(TempPositions, 0);
        cToGenerate.Clear();

        foreach (Vector3i pos in TempPositions)
        {

            if (wChunks.Get(pos) == null)
            {
                Vector3i TempPos = pos * Chunk.cSize;

                GameObject tempChunkObject = Instantiate(chunkPrefab) as GameObject;
                tempChunkObject.transform.SetParent(this.transform);
                tempChunkObject.name = TempPos.ToString();
                Chunk tempChunkScript = tempChunkObject.GetComponent<Chunk>();

                tempChunkScript.world = this;
                tempChunkScript.cPosition = TempPos;
                tempChunkScript.cVoxels = new Octree<Voxel>(Chunk.cSize, tempChunkScript.cPosition.ToVector3() + new Vector3(Chunk.cSize / 2, Chunk.cSize / 2, Chunk.cSize / 2), 16);

                cGenerator.Generate(this, tempChunkScript);
                wChunks.Add(tempChunkScript, TempPos);
            }
        }
        yield return new WaitForEndOfFrame();
    }
}
