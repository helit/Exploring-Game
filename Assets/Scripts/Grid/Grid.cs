using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
  [SerializeField]
  bool showGrid = true;

  // References
  public LevelSettings levelSettings;
  public GameObject map;

  GridData gridData;

  void Start()
  {
    gridData = GenerateGrid(showGrid);
  }

  void Update()
  {
    // Left Click
    if (Input.GetMouseButtonDown(0))
    {
      // TODO: Calculate this correctly
      Vector3 mousePos = Helpers.GetMouseWorldPosition();
      int mousePosX = Mathf.FloorToInt(mousePos.x / levelSettings.mapScale);
      int mousePosY = Mathf.FloorToInt(mousePos.z / levelSettings.mapScale);

      Debug.Log("x: " + mousePosX + " y: " + mousePosY);
    }
  }

  void OnValidate()
  {
    gridData = GenerateGrid(showGrid);
  }

  GridData GenerateGrid(bool showGrid)
  {
    if (levelSettings.drawMode == LevelSettings.DrawMode.Terrain)
    {
      return GridGenerator.GenerateGrid(
        levelSettings.mapWidth,
        levelSettings.mapHeight,
        levelSettings.mapScale,
        levelSettings.noiseMap,
        levelSettings.heightMultiplier,
        showGrid
      );
    }

    return GridGenerator.GenerateFlatGrid(
      levelSettings.mapWidth,
      levelSettings.mapHeight,
      levelSettings.mapScale,
      showGrid
    );
  }
}
