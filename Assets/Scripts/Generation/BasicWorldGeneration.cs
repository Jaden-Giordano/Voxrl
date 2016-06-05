using UnityEngine;
using SimplexNoise;

public class BasicWorldGeneration : GeneratorBase {

	public override void GenerateChunk() {
		for (int x = chunk.cPosition.x; x < chunk.cPosition.x + Chunk.cSize; x++) {
			for (int z = chunk.cPosition.z; z < chunk.cPosition.z + Chunk.cSize; z++) {
				for (int y = chunk.cPosition.y; y < chunk.cPosition.y + Chunk.cSize; y++) {
                    if(y == 0)
                    {
                        if (chunk.GetVoxel(new Vector3i(x,y,z)) == null)
                            chunk.SetVoxel(new Vector3i(x, y, z), Color.green);
                    }
				}
			}
		}
		chunk.cGenerated = true;
        Logger.Instance.OutputLog();
	}

	private static int GetNoise(int x, int y, int z, float scale, int max) {
		return Mathf.FloorToInt ((Noise.Generate (x * scale, y * scale, z * scale) + 1f) * (max / 2f));
	}
}
