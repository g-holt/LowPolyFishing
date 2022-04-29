using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;

    GameObject bait;
    Vector3 fishDistance;
    FixedJoint fixedJoint;
    Vector3 startPos;
    Quaternion startRot;

    float newZPos;
    bool stopMovement;
    public bool onHook;


    void Awake()
    {
        onHook = false;
        startPos = transform.position;
        startRot = transform.rotation;

        bait = GameObject.FindGameObjectWithTag("Bait");
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
            //GetComponent<Rigidbody>().useGravity = true;
        }    
    }

    
    public void FollowBait()
    {
        if(!onHook) { return; }

        transform.LookAt(bait.transform, transform.up);
        
        if(stopMovement) { return; }
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }


    public void ResetFish()
    {Debug.Log("FishMovementResetFish()");
        onHook = false;
        stopMovement = false;
        transform.position = startPos;
        transform.rotation = startRot;
        gameObject.SetActive(false);
    }
}
