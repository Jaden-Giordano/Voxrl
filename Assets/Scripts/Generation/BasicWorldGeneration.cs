using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase
{
    private float Frequency = 0.008f;
    private int Max = 32;

    public override void GenerateChunk()
    {
        for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cSize; x++)
        {
            for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cSize; z++)
            {
                int height = 0;
                height += GetNoise(x, 0, z, Frequency, Max);
                for (int y = chunk.cPosition.y; y < chunk.cPosition.y + Chunk.cSize; y++)
                {
                    if(y<= height)
                    {
                        if (chunk.GetVoxel(new Vector3i(x, y, z)) == null)
                        {
                            Voxel tVoxel = new Voxel();
                            tVoxel.vColor = Color.green;
                            chunk.SetVoxel(new Vector3i(x, y, z), tVoxel);
                        }
                    }
                }
            }
        }
        Logger.Instance.OutputLog();
        chunk.cGenerated = true;
    }

    private static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }
}
