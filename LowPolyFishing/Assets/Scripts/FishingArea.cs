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
            playerFishing.canFish = true;
        }    
    }


    void OnTriggerEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            playerFishing.canFish = false;
        }
    }

}
