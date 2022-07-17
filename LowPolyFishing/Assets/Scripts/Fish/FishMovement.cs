using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] GameObject bait;
    [SerializeField] float fishSpeed = 10f;
    [HideInInspector] public bool thisFish;

    Quaternion startRot;

    bool stopMovement;
    

    void Awake()
    {
        startRot = transform.rotation;
    }


    void FixedUpdate() 
    {
        if(!thisFish) { return; }
        FollowBait();    
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Bait"))
        {
            if(!thisFish) { return; }
            stopMovement = true;
        }    
    }

    
    public void FollowBait()
    {
        transform.localRotation = Quaternion.identity;
        transform.LookAt(bait.transform, transform.up);

        if(stopMovement) { return; }
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }


    public void ResetFish(Vector3 spawnPosition)
    {
        if(!thisFish) { return; }

        thisFish = false;
        stopMovement = false;

        transform.position = spawnPosition;
        transform.rotation = startRot;
    }
}
