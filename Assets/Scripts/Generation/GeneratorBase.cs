using UnityEngine;
using System.Collections;

public class GeneratorBase {

	public World world;
	public Chunk chunk;

	public void Generate(World world, Chunk chunk) {
		this.world = world;
		this.chunk = chunk;
		GenerateChunk ();
	}

	public virtual void GenerateChunk() {
	}
}
