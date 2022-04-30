using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolHandler : MonoBehaviour
{
    List<Transform> fishSchool = new List<Transform>();
    List<FishMovement> fishMovement = new List<FishMovement>();
    Casting casting;
    FishMovement currentFish;
    //GameObject currentFish;

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
        //Debug.Log(fishSchool.Count + " " + fishMovement.Count);
    }


    public void SetSchool(Transform school)
    {
        foreach(Transform child in transform)
        {
            if(school.position == child.position)
            {
                //Debug.Log("SchoolSet");
                child.GetComponentInChildren<FishSchool>().thisFish = true;
                currentFish = child.Find("Fish Container").gameObject.GetComponent<FishMovement>();
                
            }
        }
    }


    public void ResetFish()
    {
        Debug.Log("Name: " + currentFish.GetComponentInParent<Transform>().name);
        currentFish.ResetFish();
        // foreach(Transform child in transform)
        // {
        //     if(child.gameObject.activeInHierarchy)
        //     {
        //         child.GetComponentInChildren<FishMovement>().ResetFish();
        //     }
        // }
        // foreach(FishMovement thisFish in fishMovement)
        // {Debug.Log("FishReset: before");
        //     thisFish.ResetFish();
        //     Debug.Log("FishReset: after");
            
        // }
    }
}
