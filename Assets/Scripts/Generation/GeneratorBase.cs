using UnityEngine;
using System.Collections;

public class GeneratorBase {

	public World world;
	public Chunk chunk;
    //public SimplexNoise noise;

	public void Generate(World world, Chunk chunk) {
		this.world = world;
		this.chunk = chunk;
        //noise = new SimplexNoise(world.seed.ToString());

        for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cSize; x++)
        {
            for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cSize; z++)
            {
                GenerateColumn(x,z);
            }
        }
        chunk.cGenerated = true;
    }

	public virtual void GenerateColumn(int x, int z) {
	}
}
