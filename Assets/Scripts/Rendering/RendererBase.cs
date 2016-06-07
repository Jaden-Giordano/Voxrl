using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface RendererBase {
    
    void Initialize();

    void Render(World world, Chunk chunk);

    Mesh ToMesh(Mesh mesh);
    Mesh ToCollisionMesh(Mesh mesh);
}