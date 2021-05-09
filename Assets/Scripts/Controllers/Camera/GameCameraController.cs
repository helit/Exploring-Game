using UnityEngine;

public class GameCameraController : MonoBehaviour
{
  public enum StartPosition { SpawnPoint, Center, Zero };
  public StartPosition startPosition;
  public enum CameraBounds { None, Map, Custom };
  public CameraBounds cameraBounds;

  public float panSpeed = 50f;
  public float panBorderThickness = 10f;
  public Vector2 panLimit = new Vector2(400, 400);

  public float rotationSpeed = 20f;

  public bool useMousePan = false;
  public bool inverseZoom = false;

  public float scrollSpeed = 20f;
  public float minY = 20f;
  public float maxY = 120f;

  public LevelSettings levelSettings;

  void Start()
  {
    if (startPosition == StartPosition.SpawnPoint)
    {
      // TODO: Add when logic for spawnpoint is available
    }
    else if (startPosition == StartPosition.Center)
    {
      transform.position = new Vector3(
        (levelSettings.mapWidth * levelSettings.mapScale) / 2,
        transform.position.y,
        (levelSettings.mapHeight * levelSettings.mapScale) / 2
      );
    }
    else if (startPosition == StartPosition.Zero)
    {
      transform.position = new Vector3(
        0,
        transform.position.y,
        0
      );
    }
  }

  void Update()
  {
    CameraMove();
  }

  void CameraMove()
  {
    Vector3 pos = transform.position;

    // Move Camera in X and Y
    if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness && useMousePan)
    {
      pos.z += panSpeed * Time.deltaTime;
    }

    if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness && useMousePan)
    {
      pos.z -= panSpeed * Time.deltaTime;
    }

    if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness && useMousePan)
    {
      pos.x += panSpeed * Time.deltaTime;
    }

    if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness && useMousePan)
    {
      pos.x -= panSpeed * Time.deltaTime;
    }

    // Move camera in Y
    float scroll = Input.GetAxis("Mouse ScrollWheel");
    if (inverseZoom)
    {
      pos.y += scroll * scrollSpeed * 100f * Time.deltaTime;
    }
    else
    {
      pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
    }

    if (cameraBounds == CameraBounds.Map)
    {
      pos.x = Mathf.Clamp(pos.x, 0, (float)levelSettings.mapWidth * levelSettings.mapScale);
      pos.y = Mathf.Clamp(pos.y, minY, maxY);
      pos.z = Mathf.Clamp(pos.z, 0, (float)levelSettings.mapHeight * levelSettings.mapScale);
    }
    else if (cameraBounds == CameraBounds.Custom)
    {
      pos.x = Mathf.Clamp(pos.x, 0, panLimit.x);
      pos.y = Mathf.Clamp(pos.y, minY, maxY);
      pos.z = Mathf.Clamp(pos.z, 0, panLimit.y);
    }

    transform.position = pos;
  }
}