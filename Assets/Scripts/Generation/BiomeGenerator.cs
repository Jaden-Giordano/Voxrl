using UnityEngine;

public class BiomeGenerator : GeneratorBase
{
    private float Frequency = 0.008f;
    private int Max = 32;

    //Biome biome = new Biome();

    public override void GenerateColumn(int x, int z)
    {
        int height = 0;
        //height += GetNoise(x, 0, z, Frequency, Max);

        for (int i = 0, y = height - 3; y < height; y++, i++)
        {
            Voxel tVoxel = new Voxel();
            tVoxel.vColor = Biomes.Instance.biomes[0].vTypes[3];
            if (i == 2)
                tVoxel.vColor = Biomes.Instance.biomes[0].vTypes[0];

            if (chunk.GetVoxel(new Vector3i(x, y, z)) == null)
                chunk.SetVoxel(new Vector3i(x, y, z), tVoxel);
        }
    }
}
