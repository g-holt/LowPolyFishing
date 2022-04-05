using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    [SerializeField] float fishSpeed = 5f;

    GameObject bait;
    Vector3 fishDistance;
    
    public bool onHook;
    float newZPos;


    void Start()
    {
        onHook = false;

        bait = GameObject.FindGameObjectWithTag("Bait");
    }


    void Update() 
    {
        FollowBait();    
    }

    
    public void FollowBait()
    {
        if(!onHook) { return; }

        Debug.Log("FollowBait");
        //transform.SetPositionAndRotation(bait.transform.position, bait.transform.rotation);
        transform.LookAt(bait.transform, transform.up);
        //newZPos = transform.position.z - 5f;
        //fishDistance = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //transform.position = Vector3.MoveTowards(transform.position, bait.transform.position, fishSpeed * Time.deltaTime);
    }
}
