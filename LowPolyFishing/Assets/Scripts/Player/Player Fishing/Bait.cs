using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    FishSchool fishSchool;

    
    void Start()
    {
        fishSchool = FindObjectOfType<FishSchool>();
    }


    void OnCollisionEnter(Collision other) 
    {
        Debug.Log(other.gameObject.name.ToString());
        if(other.gameObject.CompareTag("FishContainer"))
        {
            if(other.gameObject.GetComponent<Rigidbody>() == null) { Debug.Log("Null"); }
            Debug.Log("here");
            //other.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
            //other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  Debug.Log(other.gameObject.name.ToString());
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.EnteredFishSchool();
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.ExitedFishSchool();
        }    
    }
}
