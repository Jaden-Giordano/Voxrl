using UnityEngine;
using System.Collections.Generic;

public class SpiralGen {

    public List<Vector3i> openList;
    public List<Vector3i> closedList;

    public List<Vector3i> genList;

    public int radius = 0;

    public SpiralGen(int radius) {
        this.radius = radius;

        openList = new List<Vector3i>();
        closedList = new List<Vector3i>();

        genList = new List<Vector3i>();

        PopulateOpenList();

        Generate();
    }

    private void PopulateOpenList() {
        for (int x = -radius; x <= radius; x++) {
            for (int z = -radius; z <= radius; z++) {
                if ((int)Mathf.Pow(x, 2) + (int)Mathf.Pow(z, 2) <= (int) Mathf.Pow(radius, 2))
                    openList.Add(new Vector3i(x, 0, z));
            }
        }
    }

    public void Generate() {
        if (openList.Count != 0) {
            if (!genList.Contains(openList[0]))
                GenerateChunk(openList[0]);
            closedList.Add(openList[0]);
            openList.Remove(openList[0]);
            Vector3i v = closedList[closedList.Count - 1];
            for (int x = -1; x <= 1; x++) {
                if (x == 0)
                    continue;
                for (int z = -1; z <= 1; z++) {
                    if (z == 0)
                        continue;
                    Vector3i nPos = v + new Vector3i(x, 0, z);
                    if (openList.Contains(nPos)) {
                        GenerateChunk(nPos);
                    }
                }
            }
            Generate();
        }
    }
    
    public void GenerateChunk(Vector3i pos) {
        genList.Add(pos);
        // Do chunk gen shit
    }

}
