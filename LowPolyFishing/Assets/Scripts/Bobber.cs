using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] float reelSpeed = 2f;
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
        playerFishing = GetComponent<PlayerFishing>();
    }


    void OnDisable() 
    {
        reelIn = false;    
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


    //bobber Prefab is visible or not based on pole visibility, so don't have to SetActive() it independently
    public void ThrowLine()
    {
        rb.useGravity = true;
        Vector3 bobberForce = new Vector3(transform.forward.x * horizontalCastStrength * 100, verticalCastStrength * 100, transform.forward.z * horizontalCastStrength * 100);
        rb.AddForce(bobberForce);
    }


    public void ResetBobber()
    {
        rb.useGravity = false;
        reelIn = false;
        transform.position = bobberContainer.position;
        rb.velocity = Vector3.zero;
        gameObject.SetActive(false);
    }


    void ReelInBobber()
    {
        if(!reelIn) { return; }

        if(DistanceCheck(transform.position, bobberContainer.position) <= rodToBobberDistance)
        {
            rb.useGravity = false;
        }

        if(DistanceCheck(transform.position, bobberContainer.position) <= .5)
        {
            ResetBobber();
        }

        Debug.Log(DistanceCheck(transform.position, bobberContainer.position));
        transform.position = Vector3.MoveTowards(transform.position, bobberContainer.position, reelSpeed * Time.deltaTime);
    }


    float DistanceCheck(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
}



        //transform.Translate(transform.forward * castStrength);
        //transform.position = Vector3.Lerp(transform.position, transform.forward * castStrength, castTime);