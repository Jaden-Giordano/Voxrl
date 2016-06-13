using UnityEngine;
using System.Collections;
using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public class GeneratorBase {

	public World world;
	public Chunk chunk;

    

    public void Generate(World world, Chunk chunk) {

		this.world = world;
		this.chunk = chunk;


        float offsetX = chunk.cPosition.x / Chunk.cWidth;
        float offsetZ = chunk.cPosition.z / Chunk.cWidth;

        float sampleSizeX = 1;
        float sampleSizeZ = 1;
        world.wNoise.GeneratePlanar(
            offsetX,
            offsetX + sampleSizeX,
            offsetZ,
            offsetZ + sampleSizeZ
        );

        for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cWidth; x++)
        {
            for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cWidth; z++)
            {
                GenerateColumn(x,z, world.wNoise);
            }
        }
        chunk.cGenerated = true;
    }

	public virtual void GenerateColumn(int x, int z, Noise2D noise) {
	}
}
