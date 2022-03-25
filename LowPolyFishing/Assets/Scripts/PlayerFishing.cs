using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;

    string isFishing;
    string isWalking;

    void Start()
    {
        isFishing = "IsFishing";
        isWalking = "IsWalking";

        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    
    void Update()
    {
        if(Keyboard.current.spaceKey.isPressed)
        {
            animator.SetBool(isFishing, false);
            playerMovement.isFishing = false;
        }
    }


    void OnCast()
    {
        playerMovement.isFishing = true;
        
        animator.SetBool(isWalking, false);
        animator.SetBool(isFishing, true);
    }
}
