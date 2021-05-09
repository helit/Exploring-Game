using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  [Header("Script References")]
  public LevelSettings levelSettings;

  [HideInInspector]
  public MapDisplay display;
  MeshData meshData;

  void Start()
  {
    display = FindObjectOfType<MapDisplay>();
    if (levelSettings.isIsland)
    {
      // falloffMap = GetFalloffMap();
    }

    GenerateMap();
  }

  void Update()
  {
    if (levelSettings.needsUpdate)
    {
      if (levelSettings.isIsland)
      {
        // falloffMap = GetFalloffMap();
      }

      GenerateMap();
    }
  }

  void GenerateMap()
  {
    levelSettings.noiseMap = NoiseMapGenerator.GenerateNoiseMap(
      levelSettings.mapWidth,
      levelSettings.mapHeight,
      levelSettings.seed,
      levelSettings.noiseScale,
      levelSettings.octaves,
      levelSettings.persistance,
      levelSettings.lacunarity,
      levelSettings.offset
    );

    Color[] colorMap = new Color[levelSettings.mapWidth * levelSettings.mapHeight];
    for (int y = 0; y < levelSettings.mapHeight; y++)
    {
      for (int x = 0; x < levelSettings.mapWidth; x++)
      {
        float currentHeight = levelSettings.noiseMap[x, y];
        for (int i = 0; i < levelSettings.terrainTypes.Length; i++)
        {
          if (currentHeight <= levelSettings.terrainTypes[i].height)
          {
            colorMap[y * levelSettings.mapWidth + x] = levelSettings.terrainTypes[i].color;
            break;
          }
        }
      }
    }

    if (levelSettings.drawMode == LevelSettings.DrawMode.NoiseMap)
    {
      // display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
      meshData = MeshGenerator.GenerateTerrainMesh(levelSettings.noiseMap);
      display.DrawMesh(
        meshData,
        TextureGenerator.TextureFromHeightMap(levelSettings.noiseMap)
      );
    }
    else if (levelSettings.drawMode == LevelSettings.DrawMode.ColorMap)
    {
      // display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
      meshData = MeshGenerator.GenerateTerrainMesh(levelSettings.noiseMap);
      display.DrawMesh(
        meshData,
        TextureGenerator.TextureFromColorMap(
          colorMap,
          levelSettings.mapWidth,
          levelSettings.mapHeight
        )
      );
    }
    else if (levelSettings.drawMode == LevelSettings.DrawMode.NoTexture)
    {
      meshData = MeshGenerator.GenerateTerrainMesh(levelSettings.noiseMap);
      display.DrawMeshNoTexture(
        meshData
      );
    }
    else if (levelSettings.drawMode == LevelSettings.DrawMode.Terrain)
    {
      meshData = MeshGenerator.GenerateTerrainMesh(
        levelSettings.noiseMap,
        levelSettings.heightMultiplier
      );

      display.DrawMesh(
        meshData,
        TextureGenerator.TextureFromColorMap(
          colorMap,
          levelSettings.mapWidth,
          levelSettings.mapHeight
        )
      );
    }
  }
}
