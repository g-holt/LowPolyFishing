using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerFishing : MonoBehaviour
{
    Casting casting;
    Animator animator;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    string reelAnim;
    string isFishingAnim;


    void Start()
    {
        reelAnim = "Reel";
        isFishingAnim = "IsFishing";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        casting = GetComponentInChildren<Casting>();
        playerMovement = GetComponent<PlayerMovement>();
    }


    //Casting Animation Event
    void HandleBobber()
    {
        casting.ThrowLine();
    }


    public void StopFishing()
    {
        animator.SetBool(isFishingAnim, false);
        animator.SetBool(reelAnim, false);
            
        playerInput.SwitchCurrentActionMap("Player");
        playerMovement.isFishing = false;
    }
    
}
