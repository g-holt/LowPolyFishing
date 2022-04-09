using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Casting casting;
    Reeling reeling;
    Animator animator;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    string reelAnim;
    string isFishingAnim;
    string isWalkingAnim;

    public GameObject fishingRod;
    public bool canFish;


    void Start()
    {
        reelAnim = "Reel";
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        reeling = GetComponentInChildren<Reeling>();
        casting = GetComponentInChildren<Casting>();

        fishingRod.SetActive(false);
    }


    void OnCast()
    {
        if(!canFish)
        {
            Debug.Log("You are not in a fish Zone");
            return;
        }

        playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
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
