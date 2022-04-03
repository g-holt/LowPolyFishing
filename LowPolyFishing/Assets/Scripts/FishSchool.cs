using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSchool : MonoBehaviour
{
    GameObject bait;


    void Start()
    {
        bait = GameObject.FindGameObjectWithTag("Bait");
    }

    
    void Update()
    {
        
    }


    public void EnteredFishSchool()
    {
        Debug.Log("Entered Fish School");
    }


    public void ExitedFishSchool()
    {
        Debug.Log("Exited Fish School");
    }

}
