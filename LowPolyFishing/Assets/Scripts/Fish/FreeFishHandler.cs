using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFishHandler : MonoBehaviour
{
    List<Transform> allFish = new List<Transform>();
    List<Vector3> startPositions = new List<Vector3>();

    GameObject currentFish;
    Vector3 spawnLocation;
    FishFreeSwim fishFreeSwim;

    int numSpawns;
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
            startPositions.Add(child.position);
        }

        numSpawns = startPositions.Count;
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
