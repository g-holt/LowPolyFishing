using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    [SerializeField] float castStrength = .1f;

    Casting casting;
    Reeling reeling;
    Animator animator;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    bool isCasting;
    string reelAnim;
    string isFishingAnim;
    string isWalkingAnim;
    float initCastStrength;

    public GameObject fishingRod;
    public bool canFish;


    void Start()
    {
        reelAnim = "Reel";
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        initCastStrength = castStrength;

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        reeling = GetComponentInChildren<Reeling>();
        casting = GetComponentInChildren<Casting>();

        fishingRod.SetActive(false);
    }


    void Update() 
    {
        if(!isCasting) { return; }
        CastDistance();    
    }


    void OnCast(InputValue value)
    {
        if(!canFish)
        {
            Debug.Log("You are not in a fish Zone");
            return;
        }

        if(value.isPressed)
        {
            isCasting = true;
        }
        else if(!value.isPressed)
        {
            Debug.Log("Value Up");
            isCasting = false;
            CastLine();
        }
    }


    void CastDistance()
    {
        castStrength += castStrength * Time.deltaTime;
        if(castStrength >= 5)
        {
            castStrength = 5f;
        }
        Debug.Log(castStrength);
    }


    void CastLine()
    {
        casting.horizontalCastStrength = castStrength;
        playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
        castStrength = initCastStrength;
    }


    //Casting Animation Event
    void HandleBobber()
    {
        casting.ThrowLine();
    }


    void OnReelIn(InputValue value)
    {   
        if(value.isPressed)
        {
            reeling.reelIn = true;
            reeling.SetGravity(false);
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            reeling.reelIn = false;
            reeling.SetGravity(true);
            animator.SetBool(reelAnim, false);    
        }
    }


    public void StopFishing()
    {
        animator.SetBool(isFishingAnim, false);
        animator.SetBool(reelAnim, false);
            
        playerInput.SwitchCurrentActionMap("Player");
    }
}
