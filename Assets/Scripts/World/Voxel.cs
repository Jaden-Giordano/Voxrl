using UnityEngine;
using System.Collections;

public class Voxel {
	private Color32 vColor = Color.white;

    private float vViscocity = 1f;

	public static float vSize = 1f;

	public void SetColor(Color32 color) {
		vColor = color;
	}

    public Color32 GetColor()
    {
        return vColor;
    }
}