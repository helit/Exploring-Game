using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
  // References
  public LevelSettings levelSettings;

  void Start()
  {
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
}
