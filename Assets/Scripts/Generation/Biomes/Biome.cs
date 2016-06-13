using UnityEngine;

[System.Serializable]
public class Biome
{
    public string Name;
    public Color32[] vTypes;
    public int MaxHeight;
    public int MinHeight;
    public float Scale;

    public Vector2 TempHumidPoint;

    public Biome() { }
}