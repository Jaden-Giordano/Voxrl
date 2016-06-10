using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class GeneratorBase {

	public World world;
	public Chunk chunk;

    float baseflatFrequency = 0.1f;
    float mountainFrequency = 0.2f;

    float flatScale = 0.125f;
    float flatBias = -0.75f;

    float terraintypeFrequency = 0.5f;
    float terraintypePersistence = 0.25f;

    float terrainSelectorEdgeFalloff = 0.75f;

    float finalterrainFrequency = 4.0f;
    float finalterrainPower = 0.125f;

    public void Generate(World world, Chunk chunk) {
		this.world = world;
		this.chunk = chunk;

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

        for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cWidth; x++)
        {
            for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cWidth; z++)
            {
                GenerateColumn(x,z, heightMap);
            }
        }
        chunk.cGenerated = true;
    }

	public virtual void GenerateColumn(int x, int z, Noise2D noise) {
	}
}
