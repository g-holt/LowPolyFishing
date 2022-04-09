using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;

    GameObject bait;
    Vector3 fishDistance;
    FixedJoint fixedJoint;


    public bool onHook;
    bool stopMovement;
    float newZPos;


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
            // Debug.Log("Collided with bait");
            // gameObject.AddComponent<HingeJoint>();
            // GetComponent<HingeJoint>().connectedBody = GameObject.FindGameObjectWithTag("Bait").GetComponent<Rigidbody>();
            stopMovement = true;
        }    
    }

    
    public void FollowBait()
    {
        Debug.Log(onHook);
        if(!onHook) { return; }

        Debug.Log("FollowBait");

        transform.LookAt(bait.transform, transform.up);
        if(stopMovement) {Debug.Log("StopMovement return"); return; }
        transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }
}
