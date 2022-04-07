using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reeling : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] Transform gearContainer;

    Rigidbody rb;
    Casting casting;
    Vector3 reelTowards;

    public bool reelIn;
    public bool surfaceCheck;
    public bool shorelineCheck;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        casting = GetComponent<Casting>();
    }


    void OnDisable() 
    { 
        reelIn = false;
        surfaceCheck = false;
        rb.useGravity = false; 
        shorelineCheck = false;
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }

    
    void Update()
    {
        ReelInGear();
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Shoreline"))
        {
            ShoreLineCollision();
        }        

        if(!shorelineCheck && other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }   
    }


   void ShoreLineCollision()
    {
        shorelineCheck = true;
        surfaceCheck = false;
        rb.useGravity = false;
    }


    void ReelInGear()
    {
        //Set In PlayerFishing.cs
        if(!reelIn) { return; }

        reelTowards = gearContainer.position;
        
        ReelingChecks();

        transform.position = Vector3.MoveTowards(transform.position, reelTowards, reelSpeed * Time.deltaTime);
    }


    void ReelingChecks()
    {
        if(GearToContainerDist() <= .5)
        {
            casting.ResetCast();
        }
        else if(surfaceCheck)
        {
            reelTowards.y = gearContainer.position.y - 2.5f;
        }
    }


    float GearToContainerDist()
    {
        return Vector3.Distance(transform.position, gearContainer.position);
    }


    public void SetGravity(bool state)
    {
        rb.useGravity = state;
    }
}
