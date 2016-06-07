using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase
{
    float caveFrequency = 0.025f;
    int caveSize = 40;

    public override void GenerateColumn(int x, int z)
    {
        int Height = 0;

        int temp = GetTemperature(x, z, 0.01f);
        int humid = GetTemperature(x, z, 0.01f);

        int biome = 0;

        if (temp > 50 && humid > 50)
            biome = 1;

        if (temp > 50 && humid <= 50)
            biome = 0;

        if (temp <= 50 && humid > 50)
            biome = 2;

        if (temp <= 50 && humid <= 50)
            biome = 3;

        Height += GetNoise(x, 0, z, Biomes.Instance.biomes[biome].NoiseFrequency, Biomes.Instance.biomes[biome].MaxHeight);

        if (Height < Biomes.Instance.biomes[biome].MinHeight)
            Height = Biomes.Instance.biomes[biome].MinHeight;

        for (int y = chunk.cPosition.y; y < chunk.cPosition.y + Chunk.cSize; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[0];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if (y < Height && y > Height - 5)
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
        return Mathf.FloorToInt((Noise.Generate(x* scale, -100, z* scale) + 1f) * (100 / 2f));
    }

    public static int GetHumidity(int x, int z, float scale)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, 100, z * scale) + 1f) * (100 / 2f));
    }
}
