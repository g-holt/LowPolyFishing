using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;

    GameObject bait;
    Vector3 fishDistance;
    FixedJoint fixedJoint;


    float newZPos;
    bool stopMovement;
    public bool onHook;


    void Awake()
    {
        onHook = false;
        
        bait = GameObject.FindGameObjectWithTag("Bait");
    }


    void Update() 
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

        transform.LookAt(bait.transform, transform.up);
        
        if(stopMovement) { return; }
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }
}
