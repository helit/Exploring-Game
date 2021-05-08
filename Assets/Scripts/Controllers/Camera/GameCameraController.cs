using UnityEngine;

public class GameCameraController : MonoBehaviour
{
  public float panSpeed = 50f;
  public float panBorderThickness = 10f;
  public Vector2 panLimit = new Vector2(400, 400);

  public float rotationSpeed = 20f;

  public bool useMousePan = false;
  public bool inverseZoom = false;

  public float scrollSpeed = 20f;
  public float minY = 20f;
  public float maxY = 120f;

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

    pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
    pos.y = Mathf.Clamp(pos.y, minY, maxY);
    pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

    transform.position = pos;
  }
}