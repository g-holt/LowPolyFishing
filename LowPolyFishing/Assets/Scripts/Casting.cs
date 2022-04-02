using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] Transform gearContainer;
    [SerializeField] Transform[] gearPoints;
    [SerializeField] BoxCollider bobberCollider;

    Rigidbody rb;
    BobberFloat bobberFloat;
    GameObject waterSurface;
    LineRenderer lineRenderer;
    PlayerFishing playerFishing;
    Vector3 reelTowards;

    bool surfaceCheck;
    public bool reelIn;
    float rodToWaterDistance;




    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        bobberFloat = GetComponent<BobberFloat>();
        lineRenderer = GetComponent<LineRenderer>();
        playerFishing = FindObjectOfType<PlayerFishing>();
        waterSurface = GameObject.FindGameObjectWithTag("WaterSurface");

        lineRenderer.positionCount = gearPoints.Length;
    }


    void OnDisable() 
    { 
        reelIn = false;
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }


    void Update()
    {
        DrawFishingLine();
        ReelInGear();
    }


    void DrawFishingLine()
    {
        for(int i = 0; i < gearPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, gearPoints[i].position);
        }
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater") && !other.gameObject.CompareTag("WaterSurface"))
        {Debug.Log("Reset" + " " + other.gameObject.name);
            ResetCast();
            return;
        }

        if(other.gameObject.CompareTag("Shoreline"))
        {
            surfaceCheck = false;
            rb.useGravity = false;
            return;
        }    
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(!rb.useGravity && other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }    
    }


    public void ThrowLine()
    {
        rb.useGravity = true;

        float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 castForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(castForce);
    }


    public void ResetCast()
    {
        bobberFloat.isFloating = false;

        playerFishing.StopFishing();
        playerFishing.fishingRod.SetActive(false);
        gameObject.SetActive(false);
    }


    void ReelInGear()
    {
        //Set In PlayerFishing.cs
        if(!reelIn) { return; }

        reelTowards = gearContainer.position;

        if(GearToContainerDist() <= .5)
        {
            ResetCast();
        }

        if(surfaceCheck)
        {
            reelTowards.y = gearContainer.position.y - 2.5f;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, reelTowards, reelSpeed * Time.deltaTime);
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




//BobberFloating    
    // void OnTriggerEnter(Collider other) 
    // {
    //     if(other.gameObject.CompareTag("WaterSurface"))
    //     {
    //         rb.velocity = Vector3.zero;
    //         rb.useGravity = false;
    //         bobberFloat.isFloating = true;
    //         float newYPos = transform.position.y + .25f;
    //         transform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    //         return;
    //     }
    // }
