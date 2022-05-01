using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;
    [SerializeField] GameObject bait;

    Vector3 startPos;
    Quaternion startRot;
    Vector3 fishDistance;
    FixedJoint fixedJoint;

    float newZPos;
    bool stopMovement;
    public bool onHook;


    void Awake()
    {
        onHook = false;
        startPos = transform.position;
        startRot = transform.rotation;
    }


    void FixedUpdate() 
    {
        FollowBait();    
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Bait"))
        {
            stopMovement = true;
        }    
    }

    
    public void FollowBait()
    {
        if(!onHook) { return; }

        transform.localRotation = Quaternion.identity;
        transform.LookAt(bait.transform, transform.up);
        
        if(stopMovement) { return; }
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }


    public void ResetFish()
    {
        onHook = false;
        stopMovement = false;
        transform.position = startPos;
        transform.rotation = startRot;
        gameObject.SetActive(false);
    }
}
