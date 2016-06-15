using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CielaSpike;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class World : MonoBehaviour
{
    public Noise2D wNoise;

    #region Noise Generation
    void GenerateNoise()
    {
        float baseflatFrequency = 0.1f;
        float mountainFrequency = 0.2f;

        float flatScale = 0.125f;
        float flatBias = 0;

        float terraintypeFrequency = 0.5f;
        float terraintypePersistence = 0.25f;

        float terrainSelectorEdgeFalloff = 0.75f;

        float finalterrainFrequency = 4.0f;
        float finalterrainPower = 0.125f;

        RidgedMultifractal mountainTerrain = new RidgedMultifractal();
        mountainTerrain.Frequency = mountainFrequency;
        mountainTerrain.Seed = World.seed;

        Billow baseFlatTerrain = new Billow();
        baseFlatTerrain.Frequency = baseflatFrequency;
        baseFlatTerrain.Seed = World.seed;

        ScaleBias flatTerrain = new ScaleBias(flatScale, flatBias, baseFlatTerrain);

        Perlin terrainType = new Perlin();
        terrainType.Frequency = terraintypeFrequency;
        terrainType.Persistence = terraintypePersistence;
        terrainType.Seed = World.seed;

        Select terrainSelector = new Select(flatTerrain, mountainTerrain, terrainType);
        terrainSelector.SetBounds(0, 1000);
        terrainSelector.FallOff = terrainSelectorEdgeFalloff;

        Turbulence finalTerrain = new Turbulence(terrainSelector);
        finalTerrain.Frequency = finalterrainFrequency;
        finalTerrain.Power = finalterrainPower;

        ModuleBase myModule;
        myModule = terrainSelector;

        wNoise = new Noise2D(32, 32, myModule);
    }
    #endregion

    public static int seed = 0;

    public Dictionary<Vector3i, Chunk> wChunks;
    
    private Queue<Vector3i> cToGenerate = new Queue<Vector3i>();
    private Queue<Vector3i> cToRemove = new Queue<Vector3i>();

    GeneratorBase cGenerator;
    public bool cGenerate = true;

    public Vector3 chunkSpawnPos;

    public GameObject chunkPrefab;

    public Vector3 worldSize;

    void Awake()
    {
        GenerateNoise();

        wChunks = new Dictionary<Vector3i, Chunk>();

        cGenerator = new BasicWorldGeneration();

        if (seed == 0)
            seed = (int)System.DateTime.Now.Ticks;
    }

    void FixedUpdate()
    {
        if (cGenerate)
        {
            cGenerate = false;
            AddChunk(new Vector3i(chunkSpawnPos));
        }

        if (cToGenerate.Count > 0) 
            StartCoroutine(CreateChunk());
        if (cToRemove.Count > 0) 
            StartCoroutine(RemoveChunk());
    }
    
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
        pos.x = (pos.x / Chunk.cWidth) * Chunk.cWidth;
        pos.y = (pos.y / Chunk.cHeight) * Chunk.cHeight;
        pos.z = (pos.z / Chunk.cWidth) * Chunk.cWidth;

        Chunk tempChunk = null;
        wChunks.TryGetValue(pos, out tempChunk);
        return tempChunk;
    }

    public void AddChunk(Vector3i pos)
    {
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

                yield return Ninja.JumpToUnity;
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
