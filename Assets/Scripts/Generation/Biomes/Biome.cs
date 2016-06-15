using UnityEngine;
using LibNoise;

public enum NoiseType
{
    Perlin,
    Billow,
    RidgedFractal
};

[System.Serializable]
public class Biome
{
    public string Name;
    public Color32[] vTypes;

    public NoiseType noiseType;

    public ModuleBase noise;
    public Noise2D noise2D;
    public double noiseFrequency;
    public double noisePersistence;
    public double noiseLacunarity;
    public int noiseOctaves;
    public int noiseScale;

    public Vector2 TempRange;
    public Vector2 HumidRange;


    public Biome() { }
}