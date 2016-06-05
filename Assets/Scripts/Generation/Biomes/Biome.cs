using UnityEngine;
using System.Collections;

public class Biome {

    public float Frequency = 0.008f;

    /*Base Colors - Default Plains Biome
     * Grass
     * Liquid Shallow
     *        Deep
     * Stone
     */
    public Color32[] baseColors = new Color32[4] {
        Color.green,
        new Color(0.239f, 0.627f, 0.627f),
        new Color(0.027f, 0.282f, 0.282f, 0.5f),
        Color.gray
    };    
}
