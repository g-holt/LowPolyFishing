using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFishing : MonoBehaviour
{
    [SerializeField] GameObject bobber;

    Animator animator;
    PlayerInput playerInput;
    PlayerMovement playerMovement;

    string reelAnim;
    string isFishingAnim;
    string isWalkingAnim;

    public GameObject fishingRod;


    void Start()
    {
        reelAnim = "Reel";
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        fishingRod.SetActive(false);
        bobber.SetActive(false);

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

        fishingRod.SetActive(true);
        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
        ThrowLine();
    }


    void OnReelIn(InputValue value)
    {   
        if(value.isPressed)
        {
            animator.SetBool(reelAnim, true);    
        }
        else if(!value.isPressed)
        {
            animator.SetBool(reelAnim, false);    
        }
    }


    public void ThrowLine()
    {
        Debug.Log("Line Out");
        bobber.SetActive(true);
    }
}
