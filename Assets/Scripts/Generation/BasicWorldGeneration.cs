using UnityEngine;
using SimplexNoise;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class BasicWorldGeneration : GeneratorBase
{
    float caveFrequency = 0.025f;
    int caveSize = 40;

    public override void GenerateColumn(int x, int z, Noise2D noise)
    {
        float[,] heightData = noise.GetNormalizedData();

        int tx = x - chunk.cPosition.x, tz = z - chunk.cPosition.z;

        int Height = (int)(heightData[tx, tz]*320);
        
        for(int y = chunk.cPosition.y; y < chunk.cPosition.y + Chunk.cWidth; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Color.green;
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
                n++;
            }
            if(y < Height)
            {
                tVox.vColor = Color.gray;
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
                n++;
            }
        }
    }

    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }

    public static int GetTemperature(int x, int z, float scale)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, -100, z * scale) + 1f) * (100 / 2f));
    }

    public static int GetHumidity(int x, int z, float scale)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, 100, z * scale) + 1f) * (100 / 2f));
    }
}
