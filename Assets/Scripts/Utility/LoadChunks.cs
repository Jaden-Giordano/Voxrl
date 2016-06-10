using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadChunks : MonoBehaviour
{

    public World world;

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


    public HashSet<Vector3i> loadedChunks = new HashSet<Vector3i>();
    public HashSet<Vector3i> cToDelete;

    public int chunkLoadDistance = 2;
    Vector3i objPos = new Vector3i();

    private void Update()
    {
        objPos.x = Mathf.FloorToInt(transform.position.x / Chunk.cWidth);
        objPos.y = 0;
        objPos.z = Mathf.FloorToInt(transform.position.z / Chunk.cWidth);

        DeleteChunks();
        LChunks();
    }

    public void DeleteChunks()
    {
        cToDelete = new HashSet<Vector3i>();
        foreach (Vector3i cPos in loadedChunks)
        {
            if (Mathf.Pow(cPos.x - objPos.x, 2) + Mathf.Pow(cPos.z - objPos.z, 2) > chunkLoadDistance)
            {
                cToDelete.Add(cPos);
            }
        }

        foreach (Vector3i cPos in cToDelete)
        {
            loadedChunks.Remove(cPos);
            world.DestroyChunk(cPos);
        }
    }

    public void LChunks()
    {
        for (int i = 0; i < chunkPositions.Length; i++)
        {
            Vector3i Pos = chunkPositions[i];
            Vector3i cPos = Pos + objPos;

            if (Mathf.Pow(Pos.x, 2) + Mathf.Pow(Pos.z, 2) < chunkLoadDistance)
            {
                if (world.GetChunk(cPos) == null && !loadedChunks.Contains(cPos))
                {
                    loadedChunks.Add(cPos);
                    world.AddChunk(cPos);
                }
            }
        }
    }
}