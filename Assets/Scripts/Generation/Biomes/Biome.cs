using UnityEngine;
using System.Collections;
using SimplexNoise;

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

    public Voxel[] GenerateColumn(int x, int z, int height)
    {
        Voxel[] tempVoxels = new Voxel[3];
        for (int i = 0, y = height - 1; y < height + 1; y++, i++) 
        {
            Voxel tVoxel = new Voxel();
            /*tVoxel.vColor = baseColors[3];
            if (i == 2)
                tVoxel.vColor = baseColors[0];*/
            tVoxel.vColor = Color.green;
            tempVoxels[i] = tVoxel;
        }
        return tempVoxels;
    }

    private static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }
}
