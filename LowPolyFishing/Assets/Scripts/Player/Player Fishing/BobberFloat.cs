using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFloat : MonoBehaviour
{
    [SerializeField] float frequency = 1f;
    [SerializeField] float amplitude = .5f;
    [SerializeField] float adjustBobberHeight = .08f;   //offset to make bobber float halfway in water
    [SerializeField] AudioClip lureLanding;
    [HideInInspector] public bool isFloating;

    Rigidbody rb;
    Vector3 tempPos;
    AudioSource audioSource;
    PlayerMovement playerMovement;

    float bobberHeightFix;  


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = FindObjectOfType<PlayerMovement>();
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

            audioSource.Stop();
            audioSource.clip = lureLanding;
            audioSource.PlayOneShot(audioSource.clip);

            FloatBobberOnSurface();
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
