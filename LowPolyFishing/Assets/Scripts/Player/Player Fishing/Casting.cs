using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] GameObject gear;
    [SerializeField] GameObject bobber;
    [SerializeField] GameObject bait;

    Rigidbody rb;
    BobberFloat bobberFloat;
    PlayerFishing playerFishing;
    LineRenderer lineRenderer;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        bobberFloat = GetComponent<BobberFloat>();
        lineRenderer = GetComponent<LineRenderer>();
        playerFishing = FindObjectOfType<PlayerFishing>();

        bobber.gameObject.SetActive(false);
        bait.gameObject.SetActive(false);
        lineRenderer.enabled = false;
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater") && !other.gameObject.CompareTag("FishContainer") && !other.gameObject.CompareTag("WaterSurface") && !other.gameObject.CompareTag("Fish"))
        {
            ResetCast();
        }
    }


    public void ThrowLine()
    {
        rb.useGravity = true;
        lineRenderer.enabled = true;
        bait.gameObject.SetActive(true);
        bobber.gameObject.SetActive(true);

        float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 castForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(castForce);
    }


    public void ResetCast()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        lineRenderer.enabled = false;
        bobberFloat.isFloating = false;
        transform.position = gear.transform.position;

        playerFishing.StopFishing();
        bait.SetActive(false);
        bobber.SetActive(false);
        playerFishing.fishingRod.SetActive(false);
    }
}
