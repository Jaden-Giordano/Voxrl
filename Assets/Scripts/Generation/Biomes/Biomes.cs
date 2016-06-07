using UnityEngine;
using System.Collections;

public class Biomes : MonoBehaviour {

    public static Biomes Instance;

    public Biome[] biomes;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
