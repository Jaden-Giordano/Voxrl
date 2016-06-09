using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase
{
    float caveFrequency = 0.025f;
    int caveSize = 40;

    public override void GenerateColumn(int x, int z)
    {
        int Height = 0;
        int HeightX = GetNoise(x, 0, z, Biomes.Instance.biomes[0].NoiseFrequency, Biomes.Instance.biomes[0].MaxHeight);
        int HeightZ = GetNoise(x, 0, z, Biomes.Instance.biomes[0].NoiseFrequency, Biomes.Instance.biomes[0].MaxHeight);

        int temp = GetTemperature(x, z, 0.01f);
        int humid = GetTemperature(x, z, 0.01f);

        Vector2[] bScalingFactor = new Vector2[Biomes.Instance.biomes.Length];

        //Color32 c;

        for (int i = 0; i < Biomes.Instance.biomes.Length; i++)
        {
            bScalingFactor[i].x = 1 - Mathf.Abs(Biomes.Instance.biomes[i].TempHumidPoint.x - temp) / 100;
            bScalingFactor[i].y = 1 - Mathf.Abs(Biomes.Instance.biomes[i].TempHumidPoint.y - humid) / 100;

            int tempNoise = GetNoise(x, 0, z, Biomes.Instance.biomes[i].NoiseFrequency, Biomes.Instance.biomes[i].MaxHeight);

            float tx = tempNoise * bScalingFactor[i].x;
            float tz = tempNoise * bScalingFactor[i].y;

            Height += (int)((tx + tz) / 2);
        }

        int biome = 0;

        if (temp < 50 && humid < 50)
            biome = 0;
        if (temp >= 50 && humid >= 50)
            biome = 1;
        if (temp >= 50 && humid < 50)
            biome = 2;
        if (temp < 50 && humid >= 50)
            biome = 3;

        /*
        HeightX += (int)Mathf.Lerp(GetNoise(x, 0, z, Biomes.Instance.biomes[3].NoiseFrequency, Biomes.Instance.biomes[3].MaxHeight), GetNoise(x, 0, z, Biomes.Instance.biomes[1].NoiseFrequency, Biomes.Instance.biomes[1].MaxHeight), x/64f);
        HeightZ += (int)Mathf.Lerp(GetNoise(x, 0, z, Biomes.Instance.biomes[3].NoiseFrequency, Biomes.Instance.biomes[3].MaxHeight), GetNoise(x, 0, z, Biomes.Instance.biomes[1].NoiseFrequency, Biomes.Instance.biomes[1].MaxHeight), z / 64f);
        Height = (HeightX + HeightZ) / 2;
        */
        /*if (Height < Biomes.Instance.biomes[biome].MinHeight)
            Height = Biomes.Instance.biomes[biome].MinHeight;*/

        for (int y = 0; y < Chunk.cHeight; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[0];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if (y < Height /*&& y > Height - 5*/)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[1];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if (y > Height && y < 192 && biome == 3)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[3];
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
