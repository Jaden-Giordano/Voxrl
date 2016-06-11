using UnityEngine;

[System.Serializable]
public struct Biome
{
    public string Name;
    public Color32[] vTypes;
    public int MaxHeight;
    public int MinHeight;
    public float Scale;

    public Vector2 TempHumidPoint;
    
}