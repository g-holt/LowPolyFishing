using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Casting casting;
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
        casting = GetComponentInChildren<Casting>();

        fishingRod.SetActive(false);
        casting.gameObject.SetActive(false);
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
        casting.gameObject.SetActive(true);
        casting.ThrowLine();
    }


    void OnReelIn(InputValue value)
    {   
        if(value.isPressed)
        {
            casting.reelIn = true;
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            casting.reelIn = false;
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
