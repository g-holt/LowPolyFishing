using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFishSchool : MonoBehaviour
{
    FishSchool[] fishSchool;


    void Start()
    {
        fishSchool = GetComponentsInChildren<FishSchool>();
    }


    public void CurrentFishSchool(string schoolName)
    {
        foreach(FishSchool school in fishSchool)
        {
            if(school.gameObject.name == schoolName)
            {
                school.thisFish = true;
            }
        }
    }
}
