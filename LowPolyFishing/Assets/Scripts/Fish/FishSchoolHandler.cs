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
            {Debug.Log("Set School: " + child.name);
                fishSchool = child.GetComponentInChildren<FishSchool>();
                fishSchool.thisFish = true;
                fishSchool.isFishing = true;
                currentFish = child.Find("Fish Container").GetComponent<FishMovement>();
            }
        }
    }


    public void ResetFish()
    {
        if(fishSchool == null) { Debug.Log("fischool is null");}
        if(fishSchool != null) { Debug.Log("fischool is not null");}
        
        foreach(Transform child in fishSchoolList)
        {Debug.Log("reset school");
            currentFish.ResetFish();
            fishSchool.ResetFishSchool();
            fishSchool.fishOn = false;
            fishSchool.isFishing = false;
        }
    }


    public List<Transform> GetFishSchoolList()
    {
        return fishSchoolList;
    }
}
