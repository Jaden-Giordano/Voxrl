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

    public float baseflatFrequency = 0.1f;
    public float mountainFrequency = 0.2f;

    public float flatScale = 0.125f;
    public float flatBias = 0f;

    public float terraintypeFrequency = 0.5f;
    public float terraintypePersistence = 0.25f;

    public float terrainSelectorEdgeFalloff = 0.75f;

    public float finalterrainFrequency = 4.0f;
    public float finalterrainPower = 0.125f;

    
    
	void Start () {
        //Generate();
	}
	
	void Update () {
        if (Input.GetMouseButtonUp(0))
            Generate();
	}

    void Generate()
    {
        float time = Time.realtimeSinceStartup;
        ModuleBase myModule;

        Voronoi biomes = new Voronoi();
        biomes.Seed = (int)System.DateTime.Now.Ticks;
        biomes.Frequency = 0.1;

        #region LandScape Generation
        RidgedMultifractal mountainTerrain = new RidgedMultifractal();
        mountainTerrain.Frequency = mountainFrequency;
        mountainTerrain.Seed = World.seed;

        Billow baseFlatTerrain = new Billow();
        baseFlatTerrain.Frequency = baseflatFrequency;
        baseFlatTerrain.Seed = World.seed;

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
        myModule = terrainType;

        Noise2D heightMap = new Noise2D(SizeX, SizeZ, myModule);

        Biomes.Instance.biomes[0].noise2D.GeneratePlanar(
                offsetX,
                offsetX + sampleSizeX,
                offsetZ,
                offsetZ + sampleSizeZ
                );

        texture = Biomes.Instance.biomes[0].noise2D.GetTexture(GradientPresets.Grayscale);
        texture.filterMode = FilterMode.Point;
        renderer.material.mainTexture = texture;
    }
}
