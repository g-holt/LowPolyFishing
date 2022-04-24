using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reeling : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] Transform gearContainer;
    [SerializeField] GameObject fishCaughtCanvas;

    Rigidbody rb;
    Casting casting;
    Vector3 reelTowards;

    public bool reelIn;
    public bool surfaceCheck;
    //public bool shorelineCheck;
    float gearContainerToWaterSurface = 2.5f;


    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        casting = GetComponent<Casting>();
        fishCaughtCanvas.SetActive(false);
    }


    void OnDisable() 
    { 
        reelIn = false;
        surfaceCheck = false;
        rb.useGravity = false; 
        //shorelineCheck = false;
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }

    
    void Update()
    {
        ReelInGear();
    }


    private void OnCollisionEnter(Collision other) 
    {    
        if(/*!shorelineCheck &&*/ other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }   
    }


    public void FishCaught()
    {
        fishCaughtCanvas.SetActive(true);       
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
        if(surfaceCheck)
        {
            reelTowards.y = gearContainer.position.y - gearContainerToWaterSurface;
        }
    }


    float GearToContainerDist()
    {
        return Vector3.Distance(transform.position, gearContainer.position);
    }


    public void ResetReeling()
    {
        surfaceCheck = false;
        //shorelineCheck = false;
    }


    public void SetGravity(bool state)
    {
        rb.useGravity = state;
    }


    void OnExitFishCatchUI()
    {
        fishCaughtCanvas.SetActive(false);
        casting.ResetCast();
    }
}
