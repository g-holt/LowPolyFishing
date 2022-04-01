using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] float rodToGearDistance = 2.5f;
    [SerializeField] Transform gearContainer;
    [SerializeField] Transform[] gearPoints;

    Rigidbody rb;
    LineRenderer lineRenderer;
    PlayerFishing playerFishing;

    public bool reelIn;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        lineRenderer = GetComponent<LineRenderer>();
        playerFishing = FindObjectOfType<PlayerFishing>();
        lineRenderer.positionCount = gearPoints.Length;
    }


    void OnDisable() 
    { 
        reelIn = false;
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater"))
        {
            ResetCast();
            return;
        }

        if(other.gameObject.CompareTag("Shoreline"))
        {
            rb.useGravity = false;
            return;
        }    
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
        playerFishing.StopFishing();

        playerFishing.fishingRod.SetActive(false);
        gameObject.SetActive(false);
    }


    void ReelInGear()
    {
        //Set In PlayerFishing.cs
        if(!reelIn) { return; }

        if(GearToContainerDist() <= .5)
        {
            ResetCast();
        }
        
        transform.position = Vector3.MoveTowards(transform.position, gearContainer.position, reelSpeed * Time.deltaTime);
    }


    float GearToContainerDist()
    {
        return Vector3.Distance(transform.position, gearContainer.position);
    }

}

