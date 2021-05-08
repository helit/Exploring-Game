using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling each Citizen
public class Citizen : MonoBehaviour
{
  public enum Gender { Male, Female };
  public enum Job { Worker, Scavanger };
  public enum Task { Idle, Working }

  public int id = 1;
  public string citizenName = "Unamed";
  public Gender gender;
  public int age = 30;
  public Job job;
  public int XP = 0;
  public int HP = 5;
  public Task task = Task.Idle;
}