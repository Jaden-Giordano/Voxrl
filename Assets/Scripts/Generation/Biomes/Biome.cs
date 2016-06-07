using UnityEngine;

[System.Serializable]
public class Biome
{
    public string Name;
    public Color32[] vTypes;
    public int MaxHeight;
    public int MinHeight;
    public float NoiseFrequency;

    public Biome() { }
    public Biome(int MaxHeight, int MinHeight, float NoiseFrequency)
    {
        vTypes = new Color32[16];
        this.MaxHeight = MaxHeight;
        this.MinHeight = MinHeight;
        this.NoiseFrequency = NoiseFrequency;
    }
}