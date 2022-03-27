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

    //bool isFishing;
    string isFishingAnim;
    string isWalkingAnim;

    void Start()
    {
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        player = playerInput.actions.FindActionMap("Player");
        fishing = playerInput.actions.FindActionMap("Fishing");
    }

    
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            animator.SetBool(isFishingAnim, false);
            animator.SetBool("Reel", false);
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


    void OnReelIn()
    {
        animator.SetBool("Reel", true);
    }
}
