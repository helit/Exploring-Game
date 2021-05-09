using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
  [Header("Script References")]
  public LevelSettings levelSettings;

  [Header("Object References")]
  public GameObject sea;

  [HideInInspector]
  public MapDisplay display;
  MeshData meshData;
  GridData gridData;

  void Start()
  {
    display = FindObjectOfType<MapDisplay>();
    if (levelSettings.isIsland)
    {
      // falloffMap = GetFalloffMap();
    }

    GenerateMap();
    PositionSea();
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
      PositionSea();
      levelSettings.needsUpdate = false;
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

      gridData = GridGenerator.GenerateGrid(
        levelSettings.mapWidth,
        levelSettings.mapHeight,
        levelSettings.mapScale,
        levelSettings.isDebug
      );

      display.DrawMesh(
        meshData,
        TextureGenerator.TextureFromHeightMap(levelSettings.noiseMap)
      );
    }
    else if (levelSettings.drawMode == LevelSettings.DrawMode.ColorMap)
    {
      // display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
      meshData = MeshGenerator.GenerateTerrainMesh(levelSettings.noiseMap);

      gridData = GridGenerator.GenerateGrid(
        levelSettings.mapWidth,
        levelSettings.mapHeight,
        levelSettings.mapScale,
        levelSettings.isDebug
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
    else if (levelSettings.drawMode == LevelSettings.DrawMode.NoTexture)
    {
      meshData = MeshGenerator.GenerateTerrainMesh(levelSettings.noiseMap);

      gridData = GridGenerator.GenerateGrid(
        levelSettings.mapWidth,
        levelSettings.mapHeight,
        levelSettings.mapScale,
        levelSettings.isDebug
      );

      display.DrawMeshNoTexture(
        meshData
      );
    }
    else if (levelSettings.drawMode == LevelSettings.DrawMode.Terrain)
    {
      meshData = MeshGenerator.GenerateTerrainMesh(
        levelSettings.noiseMap,
        levelSettings.heightMultiplier,
        levelSettings.heightCurve
      );

      gridData = GridGenerator.GenerateGrid(
        levelSettings.mapWidth,
        levelSettings.mapHeight,
        levelSettings.mapScale,
        levelSettings.isDebug,
        levelSettings.noiseMap,
        levelSettings.heightMultiplier,
        levelSettings.heightCurve
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

  void PositionSea()
  {
    sea.transform.position = new Vector3(
      (levelSettings.mapWidth * levelSettings.mapScale) / 2,
      0,
      (levelSettings.mapHeight * levelSettings.mapScale) / 2
    );

    sea.transform.localScale = new Vector3(
      levelSettings.mapWidth,
      0,
      levelSettings.mapHeight
    );
  }
}
