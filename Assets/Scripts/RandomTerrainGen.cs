using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTerrainGen : MonoBehaviour {
    public float TileSize = 20f;

    //The lower the numbers in the number range, the higher the hills/mountains will be...
    public float DivRange; // (35, 60);

    private Terrain terrain;

    private void Start() {
        terrain = GetComponent<Terrain>();
        GenerateTerrain();
    }

    private void GenerateTerrain() {
        var height = terrain.terrainData.heightmapHeight;
        var width = terrain.terrainData.heightmapWidth;

        var heights = new float[width, height];
        for (var i = 0; i < width; i++) {
            for (var k = 0; k < height; k++) {
                heights[i, k] = Mathf.PerlinNoise(((float) i / width) * TileSize + Time.time / 3,
                                    ((float) k / height) * TileSize + Time.time / 3) /
                                DivRange;
            }
        }

        terrain.terrainData.SetHeights(0, 0, heights);
    }
}