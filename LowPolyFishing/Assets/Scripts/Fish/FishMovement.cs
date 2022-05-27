using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;
    [SerializeField] GameObject bait;

    Vector3 startPos;
    Quaternion startRot;

    float newZPos;
    bool stopMovement;

    public bool onHook;
    public bool thisFish;

    void Awake()
    {
        onHook = false;
        startPos = transform.position;
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


    public void ResetFish()
    {
        if(!thisFish) { return; }

        thisFish = false;
        stopMovement = false;
        transform.position = startPos;
        transform.rotation = startRot;
        //gameObject.SetActive(false);
    }
}
