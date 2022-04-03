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


    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Bait"))
        {
            Debug.Log("Fish are here");
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Bait"))
        {
            Debug.Log("Fish Not Here");
        }    
    }
}
