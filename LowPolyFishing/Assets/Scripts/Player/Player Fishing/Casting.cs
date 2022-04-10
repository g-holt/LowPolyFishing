using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] GameObject gear_GO;
    [SerializeField] GameObject bobber_GO;
    [SerializeField] GameObject bait_GO;

    Bait bait;
    Rigidbody rb;
    Reeling reeling;
    FishSchool fishSchool;
    BobberFloat bobberFloat;
    LineRenderer lineRenderer;
    FishMovement fishMovement;
    PlayerFishing playerFishing;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        reeling = GetComponent<Reeling>();
        bait = GetComponentInChildren<Bait>();
        bobberFloat = GetComponent<BobberFloat>();
        lineRenderer = GetComponent<LineRenderer>();

        playerFishing = FindObjectOfType<PlayerFishing>();
        fishSchool = FindObjectOfType<FishSchool>();
        fishMovement = FindObjectOfType<FishMovement>();

        bobber_GO.gameObject.SetActive(false);
        bait_GO.gameObject.SetActive(false);
        lineRenderer.enabled = false;
    }


    private void OnCollisionEnter(Collision other) 
    {
        // if(!other.gameObject.CompareTag("Shoreline") && !other.gameObject.CompareTag("Underwater") && !other.gameObject.CompareTag("FishContainer") && !other.gameObject.CompareTag("WaterSurface") && !other.gameObject.CompareTag("Fish"))
        // {
        //     ResetCast();
        // }

        if(other.gameObject.CompareTag("Ground"))
        {
            ResetCast();
        }
    }


    public void ThrowLine()
    {Debug.Log("Gravity: " + rb.useGravity);
        rb.useGravity = true;
        lineRenderer.enabled = true;
        bait_GO.gameObject.SetActive(true);
        bobber_GO.gameObject.SetActive(true);

        float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 castForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(castForce);
    }


    public void ResetCast()
    {
        if(fishSchool.fishOn)
        {
            bait.ResetBait();
            fishMovement.ResetFish();
        }

        playerFishing.StopFishing();
        HandleReset();
        reeling.ResetReeling();
    }


    void HandleReset()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        lineRenderer.enabled = false;
        bobberFloat.isFloating = false;
        transform.position = gear_GO.transform.position;

        bait_GO.SetActive(false);
        bobber_GO.SetActive(false);
        playerFishing.fishingRod.SetActive(false);
    }

}
