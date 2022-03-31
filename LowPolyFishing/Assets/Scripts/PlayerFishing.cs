using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Animator animator;
    PlayerInput playerInput;
    PlayerMovement playerMovement;
    Bobber bobber;

    string reelAnim;
    string isFishingAnim;
    string isWalkingAnim;

    public GameObject fishingRod;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        bobber = GetComponentInChildren<Bobber>();

        reelAnim = "Reel";
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        fishingRod.SetActive(false);
        bobber.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            animator.SetBool(isFishingAnim, false);
            animator.SetBool(reelAnim, false);
            
            playerInput.SwitchCurrentActionMap("Player");
        }
    }


    void OnCast()
    {
        playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
    }


    //Casting Animation Event
    void HandleBobber()
    {
        bobber.gameObject.SetActive(true);
        bobber.ThrowLine();
    }


    void OnReelIn(InputValue value)
    {   
        if(value.isPressed)
        {
            bobber.reelIn = true;
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            bobber.reelIn = false;
            animator.SetBool(reelAnim, false);    
        }
    }

}
