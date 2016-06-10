using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface RendererBase {
    
    void Initialize(World world, Chunk chunk);

    void ReduceMesh();
    
    Mesh ToMesh(Mesh mesh);
    Mesh ToCollisionMesh(Mesh mesh);
}