using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for handling and referencing the whole population
public class Population : MonoBehaviour
{
  public Citizen citizen;
  public List<Citizen> people = new List<Citizen>();
  [Range(1, 10)]
  public int spawnAmount = 5;
  [Range(1f, 25f)]
  public float spawnRadius = 5f;

  void Start()
  {
    for (int i = 0; i < spawnAmount; i++)
    {
      CreateNewAdultCitizen();
    }
  }

  void Update()
  {

  }

  public void CreateNewAdultCitizen()
  {
    Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;

    Citizen newCitizen = Instantiate(
      citizen,
      new Vector3(spawnPosition.x, 0, spawnPosition.y),
      Quaternion.identity,
      transform
    );

    Citizen.Gender gender = Helpers.GetRandomEnum<Citizen.Gender>();
    string name = "";

    if (gender == Citizen.Gender.Male)
    {
      name += CitizenNames.GetRandomMaleName();
    }
    else
    {
      name += CitizenNames.GetRandomFemaleName();
    }

    name += " " + CitizenNames.GetRandomLastName();

    newCitizen.id = GetNewCitizenId();
    newCitizen.name = name;
    newCitizen.citizenName = name;
    newCitizen.gender = gender;
    newCitizen.age = GetRandomAdultAge();
    newCitizen.job = Helpers.GetRandomEnum<Citizen.Job>();
    newCitizen.XP = GetRandomXP();
    newCitizen.HP = 100;
    newCitizen.task = Citizen.Task.Idle;

    people.Add(newCitizen);
  }

  private int GetNewCitizenId()
  {
    return people.Count + 1;
  }

  private int GetRandomAdultAge()
  {
    return Random.Range(18, 55);
  }

  private int GetRandomXP()
  {
    int randomXP = Random.Range(0, 2000);
    int numSteps = (int)Mathf.Floor(randomXP / 10);
    return numSteps * 10;
  }
}
