using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingArea : MonoBehaviour
{
    Casting casting;

    void Start()
    {
        casting = FindObjectOfType<Casting>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            casting.canFish = true;Debug.Log("Entered: " + casting.canFish);
        }    
    }


    void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            casting.canFish = false;Debug.Log("Exit: " + casting.canFish);
        }
    }

}
