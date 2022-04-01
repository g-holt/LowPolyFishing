using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingArea : MonoBehaviour
{
    PlayerFishing playerFishing;

    void Start()
    {
        playerFishing = FindObjectOfType<PlayerFishing>();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Fishing Area Entered");
            playerFishing.canFish = true;
        }    
    }


    void OnTriggerEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exited Fishing Area");
            playerFishing.canFish = false;
        }
    }

}
