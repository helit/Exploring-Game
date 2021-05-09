using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Level Settings")]
public class LevelSettings : ScriptableObject
{
  public enum DrawMode { NoiseMap, ColorMap, NoTexture, Terrain };

  [Header("Draw mode")]
  public DrawMode drawMode;

  [Header("Map Settings")]
  public int mapWidth = 10;
  public int mapHeight = 10;
  public float heightMultiplier = 10;
  public float mapScale = 10;
  public int seed = 0;
  public AnimationCurve heightCurve;
  public Gradient terrainGradient;
  public TerrainType[] terrainTypes;
  public bool isIsland;

  [Header("Noise Texture Settings")]
  public float noiseScale = 15f;
  public int octaves = 4;
  [Range(0, 1)]
  public float persistance = 0.5f;
  public float lacunarity = 2;
  public Vector2 offset;

  // [Header("Falloff Texture Settings")]
  // public float falloffA = 1.8f;
  // public float falloffB = 3.5f;
  // public float[,] falloffMap;

  [HideInInspector]
  public bool needsUpdate;
  [HideInInspector]
  public float[,] noiseMap;

  void Update()
  {
    if (needsUpdate)
    {
      needsUpdate = false;
    }
  }

  void OnValidate()
  {
    needsUpdate = true;

    if (mapWidth < 1)
    {
      mapWidth = 1;
    }

    if (mapWidth < 1)
    {
      mapWidth = 1;
    }

    if (lacunarity < 1)
    {
      lacunarity = 1;
    }

    if (octaves < 0)
    {
      octaves = 0;
    }
  }
}

[System.Serializable]
public class TerrainType
{
  public string name;
  public float height;
  public Color color;
}
