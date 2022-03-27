using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Animator animator;
    InputActionMap player;
    InputActionMap fishing;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    string reelAnim;
    string isFishingAnim;
    string isWalkingAnim;

    void Start()
    {
        reelAnim = "Reel";
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
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

        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
    }


    void OnReelIn(InputValue value)
    {
        if(!animator.GetBool(isFishingAnim)) { return; }

        if(value.isPressed)
        {
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            animator.SetBool(reelAnim, false);    
        }
    }
}
