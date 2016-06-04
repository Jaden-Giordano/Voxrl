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

    void Awake()
    {
        wChunks = new Octree<Chunk>(wSize, Vector3i.zero, wMinSize);

        cGenerator = new BasicWorldGeneration();
    }

    void FixedUpdate()
    {
        if (cGenerate)
        {
            AddChunk(cGenerationPos);
            //AddChunk(new Vector3i(0, 0, 1));
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

    public void SetVoxel(Vector3i pos, Color32 color)
    {
        Chunk tempChunk = GetChunk(pos);
        if (tempChunk != null)
            tempChunk.SetVoxel(pos, color);
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
                cGenerator.Generate(this, tempChunkScript);
                wChunks.Add(tempChunkScript, TempPos);
            }
        }
        yield return new WaitForEndOfFrame();
    }
}
