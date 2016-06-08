using UnityEngine;
using SimplexNoise;

public class BiomeGenerator : GeneratorBase
{
    private float Frequency = 0.008f;
    private int Max = 32;

    Biome biome = new Biome();

    public override void GenerateColumn(int x, int z)
    {
        int height = 0;
        height += GetNoise(x, 0, z, Frequency, Max);

        for (int i = 0, y = height - 3; y < height; y++, i++)
        {
            Voxel tVoxel = new Voxel();
            tVoxel.vColor = biome.baseColors[3];
            if (i == 2)
                tVoxel.vColor = biome.baseColors[0];

            if (chunk.GetVoxel(new Vector3i(x, y, z)) == null)
                chunk.SetVoxel(new Vector3i(x, y, z), tVoxel);
        }
    }

    private static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }
}
