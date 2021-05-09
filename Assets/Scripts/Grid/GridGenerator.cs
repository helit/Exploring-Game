using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridGenerator
{
  public static GridData GenerateGrid(int mapWidth, int mapHeight, float scale, bool isDebug = false, float[,] noiseMap = null, float heightMultiplier = 0)
  {
    GridData gridData = new GridData(mapWidth, mapHeight);

    // Remove one from width and height to only include cells
    int gridWidth = mapWidth - 1;
    int gridHeight = mapHeight - 1;

    int cellIndex = 0;
    for (int y = 0; y < gridHeight; y++)
    {
      for (int x = 0; x < gridWidth; x++)
      {
        float height = 0;
        // Apply height if noise map is set
        if (noiseMap != null)
        {
          height = noiseMap[x, y] * heightMultiplier;
        }
        Vector3 cellPosition = new Vector3(x * scale, height, y * scale);
        GridCell gridCell = new GridCell(x, y, cellPosition, "[" + x + "," + y + "]", scale);
        gridData.AddCell(x, y, gridCell);

        if (isDebug)
        {
          DrawGridLines(x, y, scale, noiseMap, heightMultiplier);
        }

        cellIndex++;
      }
    }

    return gridData;
  }

  static void DrawGridLabels(int x, int y, Vector3 cellPosition, float scale)
  {
    // Center label in cell
    Vector3 labelPosition = new Vector3(cellPosition.x + scale / 2, 0, cellPosition.z + scale / 2);

    Helpers.CreateWorldText(
      x + " : " + y,
      labelPosition,
      Quaternion.Euler(90f, 0, 0),
      Color.green
    );
  }

  static void DrawGridLines(int x, int y, float scale, float[,] noiseMap = null, float heightMultiplier = 0)
  {
    float currentHeight = 0;
    float nextHeightX = 0;
    float nextHeightY = 0;

    // Apply height if noise map is set
    if (noiseMap != null)
    {
      currentHeight = noiseMap[x, y] * heightMultiplier;
      nextHeightX = noiseMap[x + 1, y] * heightMultiplier;
      nextHeightY = noiseMap[x, y + 1] * heightMultiplier;
    }

    Debug.DrawLine(
      new Vector3(x * scale, currentHeight, y * scale),
      new Vector3((x + 1) * scale, nextHeightX, y * scale),
      Color.green,
      100f
    );

    Debug.DrawLine(
      new Vector3(x * scale, currentHeight, y * scale),
      new Vector3(x * scale, nextHeightY, (y + 1) * scale),
      Color.green,
      100f
    );
  }
}

public class GridData
{
  public GridCell[,] cells;

  public GridData(int gridWidth, int gridHeight)
  {
    cells = new GridCell[gridWidth, gridHeight];
  }

  public void AddCell(int x, int y, GridCell cell)
  {
    cells[x, y] = cell;
  }
}

public class GridCell
{
  public int x;
  public int y;
  public Vector3 position;
  public string label;
  public float scale;

  public GridCell(int x, int y, Vector3 position, string label, float scale)
  {
    this.x = x;
    this.y = y;
    this.position = position;
    this.label = label;
    this.scale = scale;
  }
}

