using UnityEngine;
using SimplexNoise;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class BasicWorldGeneration : GeneratorBase
{
    float caveFrequency = 0.025f;
    int caveSize = 40;

    float baseflatFrequency = 0.1f;
    float mountainFrequency = 0.2f;

    float flatScale = 0.125f;
    float flatBias = -0.75f;

    float terraintypeFrequency = 0.5f;
    float terraintypePersistence = 0.25f;

    float terrainSelectorEdgeFalloff = 0.75f;

    float finalterrainFrequency = 4.0f;
    float finalterrainPower = 0.125f;

    public override void GenerateColumn(int x, int z)
    {
        int height = Chunk.cHeight / 2;
        float sampleSizeX = 1;
        float sampleSizeZ = 1;
        float offsetX = chunk.cPosition.x / Chunk.cWidth;
        float offsetZ = chunk.cPosition.z / Chunk.cWidth;

        RidgedMultifractal mountainTerrain = new RidgedMultifractal();
        mountainTerrain.Frequency = mountainFrequency;
        mountainTerrain.Seed = World.seed;

        Billow baseFlatTerrain = new Billow();
        baseFlatTerrain.Frequency = baseflatFrequency;

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
        myModule = finalTerrain;

        Noise2D heightMap = new Noise2D(32, 32, myModule);

        heightMap.GeneratePlanar(
            offsetX,
            offsetX + sampleSizeX,
            offsetZ,
            offsetZ + sampleSizeZ
            );

        float[,] heightData = heightMap.GetNormalizedData();

        int tx = x - chunk.cPosition.x, tz = z - chunk.cPosition.z;

        int Height = (int)(heightData[tx, tz]*Chunk.cHeight);

        chunk.SetVoxel(new Vector3i(x, Height, z), new Voxel());

        /*for(int y = 0; y < Chunk.cHeight; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Color.green;
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if(y < Height)
            {
                tVox.vColor = Color.gray;
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
        }*/

        /*int Height = Chunk.cHeight/2;
        int temp = GetTemperature(x, z, 0.001f);
        int humid = GetTemperature(x, z, 0.001f);

        Vector2[] bScalingFactor = new Vector2[Biomes.Instance.biomes.Length];
        
        /*for (int i = 0; i < Biomes.Instance.biomes.Length; i++)
        {
            bScalingFactor[i].x = 1 - Mathf.Abs(Biomes.Instance.biomes[i].TempHumidPoint.x - temp) / 100;
            bScalingFactor[i].y = 1 - Mathf.Abs(Biomes.Instance.biomes[i].TempHumidPoint.y - humid) / 100;

            int tempNoise = (int)noise.coherentNoise(x, 0, z, Biomes.Instance.biomes[i].Octaves, Biomes.Instance.biomes[i].Scale, Biomes.Instance.biomes[i].Amplitude, Biomes.Instance.biomes[i].Lacunarity, Biomes.Instance.biomes[i].Gain);

            float tx = tempNoise * bScalingFactor[i].x;
            float tz = tempNoise * bScalingFactor[i].y;

            Height += (int)((tx + tz) / 2);
        }*

        int biome = 0;

        if (temp < 50 && humid < 50)
            biome = 0;
        if (temp >= 50 && humid >= 50)
            biome = 1;
        if (temp >= 50 && humid < 50)
            biome = 2;
        if (temp < 50 && humid >= 50)
            biome = 3;


        /* What Do Dees Variabelz Do
         * x,y,z - position
         * Octaves - Number of passes
         * Multipliers - Scale
         * Amplitude - Amplitude of Noise/Wave Functions
         * Lacunarity - Varience?
         * Persistance - Noise Gain  -1 <-> 1 * Gain
         *
        Height += (int)h[x, z];
        for (int y = 0; y < Chunk.cHeight; y++)
        {
            Voxel tVox = new Voxel();
            if (y == Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[0];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            /*if (y < Height)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[1];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            if (y > Height && y < 190 && biome == 3)
            {
                tVox.vColor = Biomes.Instance.biomes[biome].vTypes[3];
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }*
        }*/
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
