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

    private void Update()
    {
        DeleteChunks();
        LChunks();
    }

    public void DeleteChunks()
    {
        cToDelete = new HashSet<Vector3i>();

        foreach (Vector3i cPos in loadedChunks)
        {
            if (Mathf.Abs(Mathf.Pow(transform.position.x + transform.position.z, 2) - Mathf.Pow(cPos.x + cPos.z, 2)) > chunkLoadDistance * Chunk.cSize)
            {
                cToDelete.Add(cPos);
            }
        }

        foreach(Vector3i cPos in cToDelete)
        {
            loadedChunks.Remove(cPos);
            world.DestroyChunk(cPos);
        }
    }

    public void LChunks()
    {
        Vector3i objPos = new Vector3i();
        objPos.x = Mathf.FloorToInt(transform.position.x / Chunk.cSize);
        objPos.y = Mathf.FloorToInt(transform.position.y / Chunk.cSize);
        objPos.z = Mathf.FloorToInt(transform.position.z / Chunk.cSize);

        foreach(Vector3i Pos in chunkPositions)
        {
            Vector3i cPos = Pos + objPos;
            for (int y = -6; y < 6; y++) 
            {
                if (!loadedChunks.Contains(cPos) && Mathf.Abs(Mathf.Pow(transform.position.x + transform.position.z, 2) - Mathf.Pow(cPos.x + cPos.z, 2)) < chunkLoadDistance * Chunk.cSize)
                {
                    loadedChunks.Add(new Vector3i(cPos.x, y, cPos.z));
                    world.AddChunk(new Vector3i(cPos.x, y, cPos.z));
                }
            }
        }
    }

}