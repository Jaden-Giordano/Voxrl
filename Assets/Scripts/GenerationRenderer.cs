using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;


public class GenerationRenderer : MonoBehaviour {

    public Renderer renderer;
    private Texture texture;

    public int SizeX = 32;
    public int SizeZ = 32;

    public float sampleSizeX = 1;
    public float sampleSizeZ = 1;
    public float offsetX = 0;
    public float offsetZ = 0;

    float baseflatFrequency = 0.1f;
    float mountainFrequency = 0.2f;

    float flatScale = 0.125f;
    float flatBias = 0f;

    float terraintypeFrequency = 0.5f;
    float terraintypePersistence = 0.25f;

    float terrainSelectorEdgeFalloff = 0.75f;

    float finalterrainFrequency = 4.0f;
    float finalterrainPower = 0.125f;

    

	// Use this for initialization
	void Start () {
        Generate();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
            Generate();
	}

    void Generate()
    {
        ModuleBase myModule;

        Voronoi biomes = new Voronoi();
        biomes.Seed = World.seed;

        #region LandScape Generation
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

        #endregion
        myModule = finalTerrain;

        Noise2D heightMap = new Noise2D(SizeX, SizeZ, myModule);

        heightMap.GeneratePlanar(
                offsetX,
                offsetX + sampleSizeX,
                offsetZ,
                offsetZ + sampleSizeZ
                );

        texture = heightMap.GetTexture(GradientPresets.Grayscale);
        texture.filterMode = FilterMode.Point;
        renderer.material.mainTexture = texture;
    }
}
