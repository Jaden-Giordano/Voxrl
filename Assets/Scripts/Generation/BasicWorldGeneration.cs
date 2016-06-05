using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase
{
    float stoneBaseHeight = -24;
    float stoneBaseNoise = 0.05f;
    float stoneBaseNoiseHeight = 4;

    float stoneMountainHeight = 48;
    float stoneMountainFrequency = 0.008f;
    float stoneMinHeight = -12;

    float dirtBaseHeight = 1;
    float dirtNoise = 0.04f;
    float dirtNoiseHeight = 3;


    Biome biome = new Biome();

    public override void GenerateColumn(int x, int z)
    {
        int stoneHeight = Mathf.FloorToInt(stoneBaseHeight);
        stoneHeight += GetNoise(x, 0, z, stoneMountainFrequency, Mathf.FloorToInt(stoneMountainHeight));

        if (stoneHeight < stoneMinHeight)
            stoneHeight = Mathf.FloorToInt(stoneMinHeight);

        stoneHeight += GetNoise(x, 0, z, stoneBaseNoise, Mathf.FloorToInt(stoneBaseNoiseHeight));

        int dirtHeight = stoneHeight + Mathf.FloorToInt(dirtBaseHeight);
        dirtHeight += GetNoise(x, 100, z, dirtNoise, Mathf.FloorToInt(dirtNoiseHeight));

        for (int y = chunk.cPosition.y; y < chunk.cPosition.y + Chunk.cSize; y++)
        {

            Voxel tVox = new Voxel();
            if (y <= stoneHeight)
            {
                tVox.vColor = Color.gray;
                chunk.SetVoxel(new Vector3i(x,y,z), tVox);
            }
            else if (y <= dirtHeight)
            {
                tVox.vColor = Color.green;
                chunk.SetVoxel(new Vector3i(x, y, z), tVox);
            }
            else
            {
                //chunk.SetVoxel(x - chunk.pos.x, y - chunk.pos.y, z - chunk.pos.z, new BlockAir());
            }

        }
    }

    public static int GetNoise(int x, int y, int z, float scale, int max)
    {
        return Mathf.FloorToInt((Noise.Generate(x * scale, y * scale, z * scale) + 1f) * (max / 2f));
    }
}
