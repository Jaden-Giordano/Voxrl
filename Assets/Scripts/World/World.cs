using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CielaSpike;

public class World : MonoBehaviour
{

    public static int seed = 0;

    public Dictionary<Vector3i, Chunk> wChunks;
    
    private Queue<Vector3i> cToGenerate = new Queue<Vector3i>();
    private Queue<Vector3i> cToRemove = new Queue<Vector3i>();

    GeneratorBase cGenerator;
    public bool cGenerate = true;

    public GameObject chunkPrefab;

    public Vector3 worldSize;

    void Awake()
    {
        wChunks = new Dictionary<Vector3i, Chunk>();

        cGenerator = new BasicWorldGeneration();

        if (seed == 0)
            seed = (int)System.DateTime.Now.Ticks;
    }

    void FixedUpdate()
    {
        if (cGenerate)
        {
            /*for (int x = -(int)worldSize.x; x < worldSize.x; x++)
            {
                for (int z = -(int)worldSize.z; z < worldSize.z; z++)
                {
                    AddChunk(new Vector3i(x, 0, z));
                }
            }*/
            AddChunk(new Vector3i(0, 0, 0));
            //AddChunk(new Vector3i(1,0,0));
        }

        StartCoroutine(CreateChunk());
        StartCoroutine(RemoveChunk());
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
        pos = (pos / Chunk.cWidth) * Chunk.cWidth;
        
        Chunk tempChunk = null;
        wChunks.TryGetValue(pos, out tempChunk);
        return tempChunk;
    }

    public void AddChunk(Vector3i pos)
    {
        cGenerate = false;
        Chunk tempChunk = GetChunk(pos);
        if(tempChunk == null)
            cToGenerate.Enqueue(pos);
    }

    public void DestroyChunk(Vector3i pos)
    {
        pos *= Chunk.cWidth;

        cToRemove.Enqueue(pos);
    }

    IEnumerator RemoveChunk()
    {
        Vector3i[] TempPositions = new Vector3i[cToRemove.Count];
        cToRemove.CopyTo(TempPositions, 0);
        cToRemove.Clear();

        foreach(Vector3i pos in TempPositions)
        {
            Chunk tempChunk = GetChunk(pos);
            if (tempChunk != null)
            {
                yield return Ninja.JumpToUnity;
                Destroy(tempChunk.gameObject);
                yield return Ninja.JumpBack;
                wChunks.Remove(pos);
            }
        }
    }

    IEnumerator CreateChunk()
    {
        Task GenerationTask;

        Vector3i[] TempPositions = new Vector3i[cToGenerate.Count];
        cToGenerate.CopyTo(TempPositions, 0);
        cToGenerate.Clear();

        foreach (Vector3i pos in TempPositions)
        {

            if (!wChunks.ContainsKey(pos))
            {
                Vector3i TempPos = pos * Chunk.cWidth;

                yield return Ninja.JumpToUnity;

                GameObject tempChunkObject = Instantiate(chunkPrefab) as GameObject;
                tempChunkObject.transform.SetParent(this.transform);
                tempChunkObject.name = (TempPos/Chunk.cWidth).ToString();
                Chunk tempChunkScript = tempChunkObject.GetComponent<Chunk>();

                yield return Ninja.JumpBack;

                tempChunkScript.world = this;
                tempChunkScript.cPosition = TempPos;
                tempChunkScript.cVoxels = new Voxel[Chunk.cWidth, Chunk.cHeight, Chunk.cWidth];

                this.StartCoroutineAsync(GenerateChunk(tempChunkScript), out GenerationTask);
                yield return StartCoroutine(GenerationTask.Wait());
                wChunks.Add(TempPos, tempChunkScript);
            }
        }
        yield break;
    }

    IEnumerator GenerateChunk(Chunk chunk)
    {
        cGenerator.Generate(this, chunk);
        yield break;
    }
}
