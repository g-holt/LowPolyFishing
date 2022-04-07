using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] GameObject bobber;
    [SerializeField] GameObject bait;

    Rigidbody rb;
    BobberFloat bobberFloat;
    PlayerFishing playerFishing;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        bobberFloat = GetComponent<BobberFloat>();

        playerFishing = FindObjectOfType<PlayerFishing>();
        bobber.gameObject.SetActive(false);
        bait.gameObject.SetActive(false);
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
        bobber.gameObject.SetActive(true);
        bait.gameObject.SetActive(true);

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
        bobber.SetActive(false);
        bait.SetActive(false);
        //gameObject.SetActive(false);
    }
}
