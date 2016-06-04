using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase
{

    public override void GenerateChunk()
    {
        for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cSize; x++)
        {
            for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cSize; z++)
            {
                if (chunk.GetVoxel(new Vector3i(x, 0, z)) == null)
                    chunk.SetVoxel(new Vector3i(x, 0, z), Color.green);

            }
        }
        chunk.cGenerated = true;
    }

    private static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }
}
