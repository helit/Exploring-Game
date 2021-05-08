using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helpers
{
  // Get the Vector3 of a clicked position in the 3D World.
  public static Vector3 GetMouseWorldPosition()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitData;

    if (Physics.Raycast(ray, out hitData, 1000))
    {
      return hitData.point;
    }

    Debug.LogWarning("GetMouseWorldPosition returned Vector3.zero");
    return Vector3.zero;
  }

  public static GameObject GetGameObjectByMousePosition()
  {
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hitData;

    if (Physics.Raycast(ray, out hitData, 1000))
    {
      return hitData.transform.gameObject;
    }

    Debug.LogWarning("GetGameObjectByMousePosition returned null");
    return null;
  }

  // Get random enum of any type
  public static T GetRandomEnum<T>()
  {
    System.Array A = System.Enum.GetValues(typeof(T));
    T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
    return V;
  }

  public static TextMesh CreateWorldText(
    string text,
    Vector3 localPosition,
    Quaternion localRotation,
    Color color,
    int fontSize = 22,
    TextAnchor textAnchor = TextAnchor.MiddleCenter,
    TextAlignment textAlignment = TextAlignment.Center,
    int sortingOrder = 0,
    Transform parent = null
  )
  {
    GameObject gameObject = new GameObject("Text Mesh", typeof(TextMesh));

    Transform transform = gameObject.transform;
    if (parent == null)
    {
      transform.SetParent(parent, false);
    }
    transform.localPosition = localPosition;
    transform.localRotation = localRotation;

    TextMesh textMesh = gameObject.GetComponent<TextMesh>();
    textMesh.anchor = textAnchor;
    textMesh.alignment = textAlignment;
    textMesh.text = text;
    textMesh.fontSize = fontSize;
    textMesh.color = color;
    textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;

    return textMesh;
  }
}
