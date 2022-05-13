using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchoolSpawner : MonoBehaviour
{
    [SerializeField] int schoolChangeTime = 30;

    List<Transform> fishSchools = new List<Transform>();

    Transform randomSchool;
    FishSchoolHandler fishSchoolHandler;

    int randomNum;

    public bool fishOn;

    void Start() 
    {
        fishOn = false;

        fishSchoolHandler = GetComponent<FishSchoolHandler>();

        PopulateList(); 
        StartCoroutine("RandomizeSpawns");   
    }


    void PopulateList()
    {
        foreach(Transform child in transform)
        {
            fishSchools.Add(child);
            child.gameObject.SetActive(false);
        }
    }


    IEnumerator RandomizeSpawns()
    {
        while(true) //Continues until game stops or object it's on is disabled
        {
            if(fishSchoolHandler.fishOn == false)
            {
                randomNum = Random.Range(0, fishSchools.Count);

                randomSchool = fishSchools[randomNum];
                randomSchool.gameObject.SetActive(true);
                Debug.Log(randomSchool.ToString());

                yield return new WaitForSeconds(schoolChangeTime);

                randomSchool.gameObject.SetActive(false);
                fishSchoolHandler.ResetFishSchoolHandler();
            }
            else
            {
                yield return null;
            }
        }

    }
}
