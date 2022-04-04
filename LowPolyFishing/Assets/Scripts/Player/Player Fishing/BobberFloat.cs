using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFloat : MonoBehaviour
{
    [SerializeField] float frequency = 1f;
    [SerializeField] float amplitude = .5f;
    [SerializeField] float adjustBobberHeight = .08f;

    Rigidbody rb;
    Reeling reeling;
    Vector3 tempPos;

    float bobberHeightFix;
    public bool isFloating;
    public bool surfaceCheck;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reeling = GetComponent<Reeling>();
    }

    
    void Update()
    {
        FloatBobber();
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(!reeling.shorelineCheck && other.gameObject.CompareTag("WaterSurface"))
        {
            FloatBobberOnSurface();
        } 
    }


    void FloatBobberOnSurface()
    {
        isFloating = true;
        surfaceCheck = true;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        bobberHeightFix = transform.position.y + adjustBobberHeight;
        transform.position = new Vector3(transform.position.x, bobberHeightFix, transform.position.z);
    }


    void FloatBobber()
    {
        if(!isFloating) { return; }
    
        tempPos = transform.position;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
