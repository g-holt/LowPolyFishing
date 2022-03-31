using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] Transform bobberContainer;
    [SerializeField] Transform[] points;

    Rigidbody rb;
    LineRenderer lineRenderer;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        lineRenderer = GetComponent<LineRenderer>();
    }


    void Update()
    {
        DrawFishingLine();
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
        transform.position = bobberContainer.position;
        rb.velocity = Vector3.zero;
    }

}



        //transform.Translate(transform.forward * castStrength);
        //transform.position = Vector3.Lerp(transform.position, transform.forward * castStrength, castTime);