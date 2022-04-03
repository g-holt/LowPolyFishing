using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] float adjustBobberHeight = .05f;
    [SerializeField] Transform gearContainer;
    [SerializeField] Transform[] gearPoints;

    Rigidbody rb;
    BobberFloat bobberFloat;
    GameObject waterSurface;
    LineRenderer lineRenderer;
    PlayerFishing playerFishing;
    Vector3 reelTowards;
    FishSchool fishSchool;

    bool surfaceCheck;
    bool shorelineCheck;
    public bool reelIn;
    float rodToWaterDistance;
    float bobberHeightFix;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        bobberFloat = GetComponent<BobberFloat>();
        lineRenderer = GetComponent<LineRenderer>();
        fishSchool = FindObjectOfType<FishSchool>();
        playerFishing = FindObjectOfType<PlayerFishing>();
        waterSurface = GameObject.FindGameObjectWithTag("WaterSurface");

        lineRenderer.positionCount = gearPoints.Length;
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
        DrawFishingLine();
        ReelInGear();
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater") && !other.gameObject.CompareTag("WaterSurface"))
        {
            ResetCast();
        }

        if(other.gameObject.CompareTag("Shoreline"))
        {
            ShoreLineCollision();
        }    

        if(!shorelineCheck && other.gameObject.CompareTag("WaterSurface"))
        {
            FloatBobberOnSurface();
        } 
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(!shorelineCheck && other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }    

        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.EnteredFishSchool();
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.ExitedFishSchool();
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


    void ShoreLineCollision()
    {
        shorelineCheck = true;
        surfaceCheck = false;
        rb.useGravity = false;
    }


    void FloatBobberOnSurface()
    {
        surfaceCheck = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        bobberFloat.isFloating = true;
        bobberHeightFix = transform.position.y + adjustBobberHeight;
        transform.position = new Vector3(transform.position.x, bobberHeightFix, transform.position.z);
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


    public void ResetCast()
    {
        bobberFloat.isFloating = false;

        playerFishing.StopFishing();
        playerFishing.fishingRod.SetActive(false);
        gameObject.SetActive(false);
    }


    float GearToContainerDist()
    {
        return Vector3.Distance(transform.position, gearContainer.position);
    }


    public void SetGravity(bool state)
    {
        rb.useGravity = state;
    }


    void DrawFishingLine()
    {
        for(int i = 0; i < gearPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, gearPoints[i].position);
        }
    }
}
