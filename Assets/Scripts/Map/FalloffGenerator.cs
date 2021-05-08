using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGenerator
{
  public static float[,] GenerateFalloffMap(int width, int height, float falloffA, float falloffB)
  {
    float[,] map = new float[width, height];

    float offset = 0.1f;

    for (int i = 0; i < width; i++)
    {
      for (int j = 0; j < height; j++)
      {
        float x = (i / (float)width * 2 - 1) + offset;
        float y = (j / (float)height * 2 - 1) + offset;

        float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
        map[i, j] = Evaluate(value, falloffA, falloffB);
      }
    }

    return map;
  }

  static float Evaluate(float value, float falloffA, float falloffB)
  {
    float a = falloffA;
    float b = falloffB;

    return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
  }
}
