using UnityEngine;
using LibNoise;
using LibNoise.Generator;

public class Biomes : MonoBehaviour {

    public static Biomes Instance;

    public Biome[] biomes;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        foreach(Biome b in biomes)
        {
            switch(b.noiseType)
            {
                case (NoiseType.Billow):
                    b.noise = new Billow(b.noiseFrequency, b.noiseLacunarity, b.noisePersistence, b.noiseOctaves, World.seed, LibNoise.QualityMode.Medium);
                    break;
                case (NoiseType.Perlin):
                    b.noise = new Perlin(b.noiseFrequency, b.noiseLacunarity, b.noisePersistence, b.noiseOctaves, World.seed, LibNoise.QualityMode.Medium);
                    break;
                case (NoiseType.RidgedFractal):
                    b.noise = new RidgedMultifractal(b.noiseFrequency, b.noiseLacunarity, b.noiseOctaves, World.seed, LibNoise.QualityMode.Medium);
                    break;
            }

            b.noise2D = new Noise2D(Chunk.cWidth, Chunk.cWidth, b.noise);
        }
    }
}
