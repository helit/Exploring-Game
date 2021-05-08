using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
  public static MeshData GenerateTerrainMesh(float[,] noiseMap, float heightMultiplier = 0)
  {
    int width = noiseMap.GetLength(0);
    int height = noiseMap.GetLength(1);

    MeshData meshData = new MeshData(width, height);
    int vertexIndex = 0;

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        meshData.vertices[vertexIndex] = new Vector3(x, noiseMap[x, y] * heightMultiplier, y);
        meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

        if (x < width - 1 && y < height - 1)
        {
          meshData.AddTriangle(vertexIndex + width, vertexIndex + width + 1, vertexIndex);
          meshData.AddTriangle(vertexIndex + 1, vertexIndex, vertexIndex + width + 1);
        }

        vertexIndex++;
      }
    }

    return meshData;
  }

  public static float[,] ApplyFalloffMap(float[,] falloffMap, float[,] noiseMap)
  {
    int width = noiseMap.GetLength(0);
    int height = noiseMap.GetLength(1);

    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < height; x++)
      {
        // int falloffX = x - (int)tilePosition.x;
        // int falloffY = y - (int)tilePosition.y;
        // noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[falloffX, falloffY]);
      }
    }

    return noiseMap;
  }
}


public class MeshData
{
  public Vector3[] vertices;
  public int[] triangles;
  public Vector2[] uvs;

  int triangleIndex;

  public MeshData(int meshWidth, int meshHeight)
  {
    vertices = new Vector3[meshWidth * meshHeight];
    uvs = new Vector2[meshWidth * meshHeight];
    triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
  }

  public void AddTriangle(int a, int b, int c)
  {
    triangles[triangleIndex] = a;
    triangles[triangleIndex + 1] = b;
    triangles[triangleIndex + 2] = c;
    triangleIndex += 3;
  }

  public Mesh CreateMesh()
  {
    Mesh mesh = new Mesh();
    mesh.vertices = vertices;
    mesh.triangles = triangles;
    mesh.uv = uvs;
    mesh.RecalculateNormals();

    return mesh;
  }
}