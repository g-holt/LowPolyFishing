using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] float rodToBobberDistance = 2.5f;
    [SerializeField] Transform bobberContainer;
    [SerializeField] Transform[] points;

    Rigidbody rb;
    LineRenderer lineRenderer;
    PlayerFishing playerFishing;

    public bool reelIn;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        lineRenderer = GetComponent<LineRenderer>();
        playerFishing = FindObjectOfType<PlayerFishing>();
        lineRenderer.positionCount = points.Length;
    }


    void OnDisable() 
    { 
        reelIn = false;
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        transform.position = bobberContainer.position;
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater"))
        {
            ResetBobber();
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
        ReelInBobber();
    }


    void DrawFishingLine()
    {
        for(int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }


    public void ThrowLine()
    {
        rb.useGravity = true;
        
        float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 bobberForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(bobberForce);
    }


    public void ResetBobber()
    {
        playerFishing.StopFishing();

        playerFishing.fishingRod.SetActive(false);
        gameObject.SetActive(false);
    }


    void ReelInBobber()
    {
        //Set In PlayerFishing.cs
        if(!reelIn) { return; }

        if(BobberToContainerDist() <= .5)
        {
            ResetBobber();
        }
        
        transform.position = Vector3.MoveTowards(transform.position, bobberContainer.position, reelSpeed * Time.deltaTime);
    }


    float BobberToContainerDist()
    {
        return Vector3.Distance(transform.position, bobberContainer.position);
    }

}

