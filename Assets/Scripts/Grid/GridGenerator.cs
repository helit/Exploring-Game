using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridGenerator
{
  public static GridData GenerateGrid(int mapWidth, int mapHeight, float scale, float[,] noiseMap, float heightMultiplier, bool isDebug = false)
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
        Vector3 cellPosition = new Vector3(x * scale, noiseMap[x, y] * heightMultiplier, y * scale);
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

  static public GridData GenerateFlatGrid(int mapWidth, int mapHeight, float scale, bool isDebug = false)
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
        Vector3 cellPosition = new Vector3(x * scale, 0, y * scale);
        GridCell gridCell = new GridCell(x, y, cellPosition, "[" + x + "," + y + "]", scale);
        gridData.AddCell(x, y, gridCell);

        if (isDebug)
        {
          DrawFlatGridLines(x, y, scale);
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

  static void DrawGridLines(int x, int y, float scale, float[,] noiseMap, float heightMultiplier)
  {
    Debug.DrawLine(
      new Vector3(x * scale, noiseMap[x, y] * heightMultiplier, y * scale),
      new Vector3((x + 1) * scale, noiseMap[x + 1, y] * heightMultiplier, y * scale),
      Color.green,
      100f
    );

    Debug.DrawLine(
      new Vector3(x * scale, noiseMap[x, y] * heightMultiplier, y * scale),
      new Vector3(x * scale, noiseMap[x, y + 1] * heightMultiplier, (y + 1) * scale),
      Color.green,
      100f
    );
  }

  static void DrawFlatGridLines(int x, int y, float scale)
  {
    Debug.DrawLine(
      new Vector3(x * scale, 0, y * scale),
      new Vector3((x + 1) * scale, 0, y * scale),
      Color.green,
      100f
    );

    Debug.DrawLine(
      new Vector3(x * scale, 0, y * scale),
      new Vector3(x * scale, 0, (y + 1) * scale),
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

