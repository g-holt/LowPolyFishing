using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolHandler : MonoBehaviour
{
    List<Transform> fishSchool = new List<Transform>();
    List<FishMovement> fishMovement = new List<FishMovement>();
    Casting casting;

    void Start()
    {
        casting = FindObjectOfType<Casting>();

        PopulateLists();
    }


    void PopulateLists()
    {
        foreach(Transform child in transform)
        {
            fishSchool.Add(child);
            fishMovement.Add(child.GetComponentInChildren<FishMovement>());
        }
    }


    public void SetSchool(Transform school)
    {
        foreach(Transform child in transform)
        {
            if(school.position == child.position)
            {
                child.GetComponentInChildren<FishSchool>().thisFish = true;
            }
        }
    }


    public void ResetFish()
    {
        foreach(FishMovement thisFish in fishMovement)
        {Debug.Log("Reset");
            thisFish.ResetFish();
        }
    }
}
