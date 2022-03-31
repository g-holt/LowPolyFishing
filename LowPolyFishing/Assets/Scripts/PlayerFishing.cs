using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Bobber bobber;
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
        bobber = GetComponentInChildren<Bobber>();

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
