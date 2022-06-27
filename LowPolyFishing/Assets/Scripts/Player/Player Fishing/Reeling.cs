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
    Animator animator;
    FishSize fishSize;
    Vector3 reelTowards;

    string reelAnim;
    float gearContainerToWaterSurface = 2.5f;

    public bool reelIn;
    public bool surfaceCheck;


    void OnEnable()
    {
        reelAnim = "Reel";

        rb = GetComponent<Rigidbody>();
        casting = GetComponent<Casting>();
        animator = GetComponentInParent<Animator>();
        fishSize = FindObjectOfType<FishSize>();

        fishCaughtCanvas.SetActive(false);
    }


    void OnDisable() 
    { 
        reelIn = false;
        surfaceCheck = false;
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }

    
    void Update()
    {
        ReelInGear();
    }


    private void OnCollisionEnter(Collision other) 
    {    
        if(other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }   
    }


    void OnReelIn(InputValue value)
    {   
        if(!surfaceCheck) { return; }

        if(value.isPressed)
        {
            reelIn = true;
            SetGravity(false);
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            reelIn = false;
            //SetGravity(true);
            animator.SetBool(reelAnim, false);    
        }
    }


    public void FishCaught()
    {
        fishCaughtCanvas.SetActive(true);   
        fishSize.SetFishSize();
    }


    void ReelInGear()
    {
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
        reelIn = false;
        surfaceCheck = false;
    }


    public void SetGravity(bool state)
    {
        rb.useGravity = state;
        Debug.Log("State: " + state.ToString() + " " + "RB: " + rb.useGravity.ToString());
    }


    void OnExitFishCatchUI()
    {
        if(!fishCaughtCanvas.activeInHierarchy) { return; }

        fishCaughtCanvas.SetActive(false);
        casting.ResetCast();
    }
}
