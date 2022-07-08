using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reeling : MonoBehaviour
{
    [SerializeField] float reelSpeed = 2f;
    [SerializeField] Transform gearContainer;
    [SerializeField] GameObject fishCaughtCanvas;
    [SerializeField] AudioClip reelingSFX;
    [SerializeField] AudioClip fishCaught;
    [HideInInspector] public bool reelIn;
    [HideInInspector] public bool surfaceCheck;

    Rigidbody rb;
    Scenes scenes;
    Casting casting;
    Animator animator;
    FishSize fishSize;
    Vector3 reelTowards;
    AudioSource audioSource;

    bool canReel;
    string reelAnim;
    float gearContainerToWaterSurface = 2.5f;


    void OnEnable()
    {
        canReel = true;
        reelAnim = "Reel";

        rb = GetComponent<Rigidbody>();
        casting = GetComponent<Casting>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInParent<Animator>();
        
        scenes = FindObjectOfType<Scenes>();
        fishSize = FindObjectOfType<FishSize>();

        fishCaughtCanvas.SetActive(false);
    }


    void OnDisable() 
    { 
        reelIn = false;
        surfaceCheck = false;
        rb.useGravity = false; 
        rb.velocity = Vector3.zero;
        transform.position = gearContainer.position;
    }

    
    void Update()
    {
        ReelInGear();
    }


    private void OnCollisionEnter(Collision other) 
    {    
        if(other.gameObject.CompareTag("WaterSurface"))
        {
            surfaceCheck = true;
        }   
    }


    void OnReelIn(InputValue value)
    {   
        if(!canReel) { return; }
        if(!surfaceCheck) { return; }

        if(value.isPressed)
        {
            reelIn = true;
            SetGravity(false);
            animator.SetBool(reelAnim, true);    
            
            audioSource.clip = reelingSFX;
            audioSource.loop = true;
            audioSource.PlayOneShot(audioSource.clip);
        }
        else if(!value.isPressed)
        {
            reelIn = false;
            animator.SetBool(reelAnim, false);    
            audioSource.loop = false;
        }
    }


    public void FishCaught()
    {
        canReel = false;
        fishCaughtCanvas.SetActive(true);   
        fishSize.SetFishSize();
        
        audioSource.Stop();
        audioSource.clip = fishCaught;
        audioSource.PlayOneShot(audioSource.clip);
    }


    void ReelInGear()
    {
        if(!reelIn) { return; }

        reelTowards = gearContainer.position;
        ReelingChecks();
        transform.position = Vector3.MoveTowards(transform.position, reelTowards, reelSpeed * Time.deltaTime);
    }


    void ReelingChecks()
    {
        if(surfaceCheck)
        {
            reelTowards.y = gearContainer.position.y - gearContainerToWaterSurface;
        }
    }


    float GearToContainerDist()
    {
        return Vector3.Distance(transform.position, gearContainer.position);
    }


    public void ResetReeling()
    {
        reelIn = false;
        canReel = true;
        surfaceCheck = false;
       
        audioSource.loop = false;
        audioSource.Stop();
    }


    public void SetGravity(bool state)
    {
        rb.useGravity = state;
    }


    void OnExitFishCatchUI()
    {
        if(!fishCaughtCanvas.activeInHierarchy) { return; }

        fishCaughtCanvas.SetActive(false);
        casting.ResetCast();

        if(fishSize.goalReached)
        {
            scenes.LoadNextScene();
        }
    }

}
