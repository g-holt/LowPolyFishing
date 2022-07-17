using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFishHandler : MonoBehaviour
{
    [HideInInspector] public bool hasFish;

    List<Vector3> startPositions = new List<Vector3>();

    Vector3 spawnLocation;
    GameObject currentFish;


    void Start()
    {
        hasFish = false;

        PopulateList();
    }


    void PopulateList()
    {
        foreach(Transform child in transform)
        {
            startPositions.Add(child.position);
        }
    }


    public void ResetFish()
    {
        if(currentFish == null) { return; }
        
        hasFish = false;
        spawnLocation = startPositions[Random.Range(0, startPositions.Count)];
        
        currentFish.GetComponent<FishFreeSwim>().ResetFishFreeSwim();
        currentFish.GetComponent<FishMovement>().ResetFish(spawnLocation);
    }


    public void SetCurrentFish(GameObject fish)
    {
        hasFish = true;
        currentFish = fish;

        currentFish.GetComponent<FishFreeSwim>().thisFish = true;
        currentFish.GetComponent<FishMovement>().thisFish = true;
    }
    
}
