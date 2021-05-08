using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseMapGenerator
{
  public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
  {
    // Generate psuedo random number based on a seed value.
    System.Random prng = new System.Random(seed);
    Vector2[] octaveOffsets = new Vector2[octaves];
    for (int i = 0; i < octaves; i++)
    {
      float offsetX = prng.Next(-100000, 100000) + offset.x;
      float offsetY = prng.Next(-100000, 100000) + offset.y;
      octaveOffsets[i] = new Vector2(offsetX, offsetY);
    }

    float[,] noiseMap = new float[mapWidth, mapHeight];

    if (scale <= 0)
    {
      scale = 0.0001f;
    }

    float maxNoiseHeight = float.MinValue;
    float minNoiseHeight = float.MaxValue;

    float halfWidth = mapWidth / 2f;
    float halfDepth = mapHeight / 2f;

    for (int y = 0; y < mapHeight; y++)
    {
      for (int x = 0; x < mapWidth; x++)
      {

        float amplitude = 1;
        float frequency = 1;
        float noiseHeight = 0;

        for (int i = 0; i < octaves; i++)
        {
          float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
          float sampleY = (y - halfDepth) / scale * frequency + octaveOffsets[i].y;

          float perlinValue = Mathf.PerlinNoise(sampleY, sampleX) * 2 - 1;
          noiseHeight += perlinValue * amplitude;

          amplitude *= persistance;
          frequency *= lacunarity;
        }

        if (noiseHeight > maxNoiseHeight)
        {
          maxNoiseHeight = noiseHeight;
        }
        else if (noiseHeight < minNoiseHeight)
        {
          minNoiseHeight = noiseHeight;
        }


        noiseMap[x, y] = noiseHeight;
      }
    }

    for (int y = 0; y < mapHeight; y++)
    {
      for (int x = 0; x < mapWidth; x++)
      {
        noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
      }
    }

    return noiseMap;
  }
}
