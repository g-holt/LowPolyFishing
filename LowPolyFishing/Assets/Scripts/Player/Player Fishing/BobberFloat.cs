using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFloat : MonoBehaviour
{
    [SerializeField] float frequency = 1f;
    [SerializeField] float amplitude = .5f;
    [SerializeField] float adjustBobberHeight = .08f;   //offset to make bobber float halfway in water
    [SerializeField] AudioClip lureLanding;

    Rigidbody rb;
    Reeling reeling;
    Vector3 tempPos;
    PlayerMovement playerMovement;
    AudioSource audioSource;

    float bobberHeightFix;  
    public bool isFloating;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        reeling = GetComponent<Reeling>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        FloatBobber();
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("WaterSurface"))
        {
            playerMovement.isFishing = true;
            playerMovement.isCasting = false;

            FloatBobberOnSurface();
            
            audioSource.Stop();
            audioSource.clip = lureLanding;
            audioSource.PlayOneShot(audioSource.clip);
        } 
    }


    void FloatBobberOnSurface()
    {
        isFloating = true;
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
