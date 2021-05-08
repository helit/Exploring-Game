using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for handling selecable objects.
public class SelectableObject : MonoBehaviour
{
  public bool isSelected = false;
  private OutlineScript outlineScript;

  void Awake()
  {
    outlineScript = GetComponent<OutlineScript>();
  }

  void ToggleSelected()
  {
    outlineScript.enabled = isSelected;
  }
}
