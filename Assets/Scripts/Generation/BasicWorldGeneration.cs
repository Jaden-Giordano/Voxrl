using UnityEngine;
using SimplexNoise;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class BasicWorldGeneration : GeneratorBase
{
    float caveFrequency = 0.025f;
    int caveSize = 40;

    public override void GenerateColumn(int x, int z)
    {
        float offsetX = chunk.cPosition.x / Chunk.cWidth;
        float offsetZ = chunk.cPosition.z / Chunk.cWidth;

        int biome = 1;

        Biomes.Instance.biomes[biome].noise2D.GeneratePlanar(
            offsetX,
            offsetX + 1,
            offsetZ,
            offsetZ + 1
        );


        float[,] heightData = Biomes.Instance.biomes[biome].noise2D.GetNormalizedData();

        int tx = x - chunk.cPosition.x, tz = z - chunk.cPosition.z;

        int Height = Chunk.cHeight / 2;

        Height += (int)(heightData[tx, tz]*Biomes.Instance.biomes[biome].noiseScale);
        
        for(int y = 0; y < Chunk.cHeight; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[0];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if(y < Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[1];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
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
