using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float horizontalCastStrength = 10f;
    [SerializeField] Transform bobberContainer;
    
    Rigidbody rb;


    void Start() 
    {
        rb = GetComponent<Rigidbody>();    
    }


    //bobber Prefab is visible or not based on pole visibility, so don't have to SetActive() it independently
    public void ThrowLine()
    {
        
        Debug.Log("Line Out: Throw Bobber");
        rb.useGravity = true;
        Vector3 facing = transform.TransformDirection(transform.position);
        Debug.Log(facing.ToString());
        Vector3 bobberForce = new Vector3(facing.x * horizontalCastStrength * 100, verticalCastStrength * 100, facing.z * horizontalCastStrength * 100);
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