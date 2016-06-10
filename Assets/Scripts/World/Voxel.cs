using UnityEngine;
using System.Collections;

public class Voxel {

	public Color32 vColor = Color.white;

    public float vViscocity = 1f;

	public static float vSize = .5f;

    public override bool Equals(object obj)
    {
        if (!(obj is Voxel))
            return false;

        Voxel tVox = (Voxel)obj;

        if (vColor.Equals(tVox.vColor))
            return true;

        return false;
    }
}