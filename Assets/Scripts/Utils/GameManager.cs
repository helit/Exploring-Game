using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public GameObject selectedGameObject;
  public GameObject mapMesh;
  public List<Vector3> spawnPositions;
  public List<Vector3> shorePositions;

  public Transform spawnPosition;
  public Transform ship;
  public Transform water;

  void Start()
  {
  }

}
