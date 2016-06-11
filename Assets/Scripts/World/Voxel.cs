using UnityEngine;
using System.Collections;

public class Voxel {

    public Voxel() { }

    public Voxel(Color32 color)
    {
        vColor = color;
    }

	public Color32 vColor;

    public float vViscocity;

	public static float vSize = 0.5f;

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