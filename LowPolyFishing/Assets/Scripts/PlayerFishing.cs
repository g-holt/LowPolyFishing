using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    bool isFishing;
    string isFishingAnim;
    string isWalkingAnim;

    void Start()
    {
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";

        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            isFishing = false;
            animator.SetBool(isFishingAnim, false);
            animator.SetFloat("ReelIn", -1);
            playerMovement.isFishing = false;
        }

        if(Keyboard.current.rKey.isPressed && isFishing)
        {
            animator.SetFloat("ReelIn", 1);
        }
    }


    void OnCast()
    {
        isFishing = true;
        playerMovement.isFishing = true;
        
        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);
    }
}
