using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFishHandler : MonoBehaviour
{
    List<Transform> allFish = new List<Transform>();

    GameObject currentFish;
    FishFreeSwim fishFreeSwim;

    public bool hasFish;


    void Start()
    {
        hasFish = false;

        PopulateList();
    }


    void PopulateList()
    {
        foreach(Transform child in transform)
        {
            allFish.Add(child);
        }
    }


    public void ResetFish()
    {
        if(currentFish == null) { return; }
        
        hasFish = false;
        currentFish.GetComponent<FishFreeSwim>().ResetFishFreeSwim();
        currentFish.GetComponent<FishMovement>().ResetFish();
    }


    public void SetCurrentFish(GameObject fish)
    {
        hasFish = true;
        currentFish = fish;
        currentFish.GetComponent<FishFreeSwim>().thisFish = true;
        currentFish.GetComponent<FishMovement>().thisFish = true;
        Debug.Log(currentFish.name);
    }
    
}
