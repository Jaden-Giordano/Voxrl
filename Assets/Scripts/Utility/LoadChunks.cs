using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadChunks : MonoBehaviour {

    public World world;

    List<Vector3i> updateList = new List<Vector3i>();
    List<Vector3i> buildList = new List<Vector3i>();

    static Vector3i[] chunkPositions = {   new Vector3i( 0, 0,  0), new Vector3i(-1, 0,  0), new Vector3i( 0, 0, -1), new Vector3i( 0, 0,  1), new Vector3i( 1, 0,  0),
                             new Vector3i(-1, 0, -1), new Vector3i(-1, 0,  1), new Vector3i( 1, 0, -1), new Vector3i( 1, 0,  1), new Vector3i(-2, 0,  0),
                             new Vector3i( 0, 0, -2), new Vector3i( 0, 0,  2), new Vector3i( 2, 0,  0), new Vector3i(-2, 0, -1), new Vector3i(-2, 0,  1),
                             new Vector3i(-1, 0, -2), new Vector3i(-1, 0,  2), new Vector3i( 1, 0, -2), new Vector3i( 1, 0,  2), new Vector3i( 2, 0, -1),
                             new Vector3i( 2, 0,  1), new Vector3i(-2, 0, -2), new Vector3i(-2, 0,  2), new Vector3i( 2, 0, -2), new Vector3i( 2, 0,  2),
                             new Vector3i(-3, 0,  0), new Vector3i( 0, 0, -3), new Vector3i( 0, 0,  3), new Vector3i( 3, 0,  0), new Vector3i(-3, 0, -1),
                             new Vector3i(-3, 0,  1), new Vector3i(-1, 0, -3), new Vector3i(-1, 0,  3), new Vector3i( 1, 0, -3), new Vector3i( 1, 0,  3),
                             new Vector3i( 3, 0, -1), new Vector3i( 3, 0,  1), new Vector3i(-3, 0, -2), new Vector3i(-3, 0,  2), new Vector3i(-2, 0, -3),
                             new Vector3i(-2, 0,  3), new Vector3i( 2, 0, -3), new Vector3i( 2, 0,  3), new Vector3i( 3, 0, -2), new Vector3i( 3, 0,  2),
                             new Vector3i(-4, 0,  0), new Vector3i( 0, 0, -4), new Vector3i( 0, 0,  4), new Vector3i( 4, 0,  0), new Vector3i(-4, 0, -1),
                             new Vector3i(-4, 0,  1), new Vector3i(-1, 0, -4), new Vector3i(-1, 0,  4), new Vector3i( 1, 0, -4), new Vector3i( 1, 0,  4),
                             new Vector3i( 4, 0, -1), new Vector3i( 4, 0,  1), new Vector3i(-3, 0, -3), new Vector3i(-3, 0,  3), new Vector3i( 3, 0, -3),
                             new Vector3i( 3, 0,  3), new Vector3i(-4, 0, -2), new Vector3i(-4, 0,  2), new Vector3i(-2, 0, -4), new Vector3i(-2, 0,  4),
                             new Vector3i( 2, 0, -4), new Vector3i( 2, 0,  4), new Vector3i( 4, 0, -2), new Vector3i( 4, 0,  2), new Vector3i(-5, 0,  0),
                             new Vector3i(-4, 0, -3), new Vector3i(-4, 0,  3), new Vector3i(-3, 0, -4), new Vector3i(-3, 0,  4), new Vector3i( 0, 0, -5),
                             new Vector3i( 0, 0,  5), new Vector3i( 3, 0, -4), new Vector3i( 3, 0,  4), new Vector3i( 4, 0, -3), new Vector3i( 4, 0,  3),
                             new Vector3i( 5, 0,  0), new Vector3i(-5, 0, -1), new Vector3i(-5, 0,  1), new Vector3i(-1, 0, -5), new Vector3i(-1, 0,  5),
                             new Vector3i( 1, 0, -5), new Vector3i( 1, 0,  5), new Vector3i( 5, 0, -1), new Vector3i( 5, 0,  1), new Vector3i(-5, 0, -2),
                             new Vector3i(-5, 0,  2), new Vector3i(-2, 0, -5), new Vector3i(-2, 0,  5), new Vector3i( 2, 0, -5), new Vector3i( 2, 0,  5),
                             new Vector3i( 5, 0, -2), new Vector3i( 5, 0,  2), new Vector3i(-4, 0, -4), new Vector3i(-4, 0,  4), new Vector3i( 4, 0, -4),
                             new Vector3i( 4, 0,  4), new Vector3i(-5, 0, -3), new Vector3i(-5, 0,  3), new Vector3i(-3, 0, -5), new Vector3i(-3, 0,  5),
                             new Vector3i( 3, 0, -5), new Vector3i( 3, 0,  5), new Vector3i( 5, 0, -3), new Vector3i( 5, 0,  3), new Vector3i(-6, 0,  0),
                             new Vector3i( 0, 0, -6), new Vector3i( 0, 0,  6), new Vector3i( 6, 0,  0), new Vector3i(-6, 0, -1), new Vector3i(-6, 0,  1),
                             new Vector3i(-1, 0, -6), new Vector3i(-1, 0,  6), new Vector3i( 1, 0, -6), new Vector3i( 1, 0,  6), new Vector3i( 6, 0, -1),
                             new Vector3i( 6, 0,  1), new Vector3i(-6, 0, -2), new Vector3i(-6, 0,  2), new Vector3i(-2, 0, -6), new Vector3i(-2, 0,  6),
                             new Vector3i( 2, 0, -6), new Vector3i( 2, 0,  6), new Vector3i( 6, 0, -2), new Vector3i( 6, 0,  2), new Vector3i(-5, 0, -4),
                             new Vector3i(-5, 0,  4), new Vector3i(-4, 0, -5), new Vector3i(-4, 0,  5), new Vector3i( 4, 0, -5), new Vector3i( 4, 0,  5),
                             new Vector3i( 5, 0, -4), new Vector3i( 5, 0,  4), new Vector3i(-6, 0, -3), new Vector3i(-6, 0,  3), new Vector3i(-3, 0, -6),
                             new Vector3i(-3, 0,  6), new Vector3i( 3, 0, -6), new Vector3i( 3, 0,  6), new Vector3i( 6, 0, -3), new Vector3i( 6, 0,  3),
                             new Vector3i(-7, 0,  0), new Vector3i( 0, 0, -7), new Vector3i( 0, 0,  7), new Vector3i( 7, 0,  0), new Vector3i(-7, 0, -1),
                             new Vector3i(-7, 0,  1), new Vector3i(-5, 0, -5), new Vector3i(-5, 0,  5), new Vector3i(-1, 0, -7), new Vector3i(-1, 0,  7),
                             new Vector3i( 1, 0, -7), new Vector3i( 1, 0,  7), new Vector3i( 5, 0, -5), new Vector3i( 5, 0,  5), new Vector3i( 7, 0, -1),
                             new Vector3i( 7, 0,  1), new Vector3i(-6, 0, -4), new Vector3i(-6, 0,  4), new Vector3i(-4, 0, -6), new Vector3i(-4, 0,  6),
                             new Vector3i( 4, 0, -6), new Vector3i( 4, 0,  6), new Vector3i( 6, 0, -4), new Vector3i( 6, 0,  4), new Vector3i(-7, 0, -2),
                             new Vector3i(-7, 0,  2), new Vector3i(-2, 0, -7), new Vector3i(-2, 0,  7), new Vector3i( 2, 0, -7), new Vector3i( 2, 0,  7),
                             new Vector3i( 7, 0, -2), new Vector3i( 7, 0,  2), new Vector3i(-7, 0, -3), new Vector3i(-7, 0,  3), new Vector3i(-3, 0, -7),
                             new Vector3i(-3, 0,  7), new Vector3i( 3, 0, -7), new Vector3i( 3, 0,  7), new Vector3i( 7, 0, -3), new Vector3i( 7, 0,  3),
                             new Vector3i(-6, 0, -5), new Vector3i(-6, 0,  5), new Vector3i(-5, 0, -6), new Vector3i(-5, 0,  6), new Vector3i( 5, 0, -6),
                             new Vector3i( 5, 0,  6), new Vector3i( 6, 0, -5), new Vector3i( 6, 0,  5) };

    int timer = 0;

    void Update()
    {
        DeleteChunks();
        FindChunksToLoad();
        LoadAndRenderChunks();
    }

    void FindChunksToLoad()
    {
        Vector3i objectPos = new Vector3i(
            Mathf.FloorToInt(transform.position.x / Chunk.cSize) * Chunk.cSize,
            Mathf.FloorToInt(transform.position.y / Chunk.cSize) * Chunk.cSize,
            Mathf.FloorToInt(transform.position.z / Chunk.cSize) * Chunk.cSize
            );

        if(buildList.Count == 0)
        {
            for (int i = 0; i < chunkPositions.Length; i++)
            {
                Vector3i newChunkPosition = new Vector3i(chunkPositions[i].x * Chunk.cSize + objectPos.x, 0, chunkPositions[i].z * Chunk.cSize + objectPos.z);

                Chunk newChunk = world.GetChunk(newChunkPosition);

                if (newChunk != null && (newChunk.cRendered || updateList.Contains(newChunkPosition)))
                    continue;

                for (int y = 0; y < 4; y++)
                {
                    buildList.Add(new Vector3i(newChunkPosition.x, y * Chunk.cSize, newChunkPosition.z));
                }
                return;
            }
        }
    }

    void LoadAndRenderChunks()
    {
        for (int i = 0; i < 2; i++)
        {
            if (buildList.Count != 0)
            {
                BuildChunk(buildList[0]);
                buildList.RemoveAt(0);
            }
        }

        for (int i = 0; i < updateList.Count; i++)
        {
            Chunk chunk = world.GetChunk(updateList[i]);
            if (chunk != null)
                chunk.cDirty = true;
            updateList.RemoveAt(i);
        }
    }

    void BuildChunk(Vector3i pos)
    {
        for (int y = pos.y - Chunk.cSize; y <= pos.y + Chunk.cSize; y+= Chunk.cSize)
        {
            if (y > 64 || y < -64)
                continue;

            for (int x = pos.x - Chunk.cSize; x <= pos.x + Chunk.cSize; x += Chunk.cSize)
            {
                for (int z = pos.z - Chunk.cSize; z <= pos.z + Chunk.cSize; z += Chunk.cSize)
                {
                    if (x > 128 || x < -128 || z > 128 || z < -128)
                        break;
                    if (world.GetChunk(new Vector3i(x, y, z)) == null)
                        world.AddChunk(new Vector3i(x, y, z));
                }
            }
        }
        updateList.Add(pos);
    }

    void DeleteChunks()
    {

        if (timer == 10)
        {
            var chunksToDelete = new List<Vector3i>();
            foreach (Vector3i chunk in world.wChunks.GetPositions())
            {
                float distance = Vector3.Distance(
                    new Vector3(chunk.x, 0, chunk.z),
                    new Vector3(transform.position.x, 0, transform.position.z));

                if (distance > 256)
                    chunksToDelete.Add(chunk);
            }

            foreach (var chunk in chunksToDelete)
                world.DestroyChunk(chunk);

            timer = 0;
        }

        timer++;
    }
}
