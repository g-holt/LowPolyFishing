using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolHandler : MonoBehaviour
{
    List<Transform> fishSchoolList = new List<Transform>();
    
    Casting casting;
    FishSchool fishSchool;
    FishMovement currentFish;


    void Start()
    {
        casting = FindObjectOfType<Casting>();

        PopulateLists();
    }


    void PopulateLists()
    {
        foreach(Transform child in transform)
        {
            fishSchoolList.Add(child);
        }
    }


    public void SetSchool(Transform school)
    {
        foreach(Transform child in fishSchoolList)
        {
            if(school.position == child.position)
            {
                fishSchool = child.GetComponentInChildren<FishSchool>();
                fishSchool.thisFish = true;
                currentFish = child.Find("Fish Container").GetComponent<FishMovement>();
                
            }
        }
    }


    public void ResetFish()
    {
        currentFish.ResetFish();
        fishSchool.fishOn = false;
    }
}
