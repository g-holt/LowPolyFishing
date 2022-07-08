using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 100f;    //Angle of Turn
    [HideInInspector] public bool fishOn;
    [HideInInspector] public bool inMenu;
    [HideInInspector] public bool isFishing;
    [HideInInspector] public bool isCasting;

    Casting casting;
    Vector2 moveInput;
    Animator animator;
    PlayerInput playerInput;
  
    float moveXPos;
    float moveZPos;
    string reelAnim;
    string isWalkingAnim;
    string isFishingAnim;


    void Start()
    {
        reelAnim = "Reel";
        isWalkingAnim = "IsWalking";
        isFishingAnim = "IsFishing";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();     
        casting = GetComponentInChildren<Casting>();
    }
    
    
    void FixedUpdate() 
    {
        if(fishOn) { return; }
        if(isCasting) { return; }
        if(inMenu) { return; }
        
        Move();    
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Dock"))
        {
            casting.canFish = true;
        }

        if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Shoreline"))
        {
            casting.canFish = false;
        }
    }


    void Move()
    {
        if(moveInput.x != 0f || moveInput.y != 0f)
        {
            moveXPos = moveInput.x * turnSpeed * Time.deltaTime;
            moveZPos = moveInput.y * moveSpeed * Time.deltaTime;
            
            if(moveZPos < 0)
            {
                moveXPos *= Mathf.Sign(moveZPos);
            }
            
            IsFishingCheck();
            
            animator.SetBool(isWalkingAnim, true);
            transform.Translate(0f, 0f, moveZPos);
            transform.Rotate(0f, moveXPos, 0f, Space.Self);
        }
        else
        {
            animator.SetBool(isWalkingAnim, false);
        }
    }


    void IsFishingCheck()
    {
        //if isFishing and player tries to walk cancel fishing
        if(isFishing)
        {   
            animator.SetBool(isFishingAnim, false);
            animator.SetBool(reelAnim, false);

            casting.ResetCast();
            casting.fishingRod.SetActive(false);
            isFishing = false;
            playerInput.SwitchCurrentActionMap("Player");
        }
    }


    void OnMove(InputValue Value)
    {
        moveInput = Value.Get<Vector2>();
    }

}





    //Player left and right rotation done with mouse

    //Vector2 mousePosition;

    // void OnMousePosition(InputValue value)
    // {
    //     mousePosition = value.Get<Vector2>();
    // }
    
    // void Move()
    // {
    //     moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

    //     transform.rotation = Quaternion.Euler(0f, mousePosition.x * maxRotationSpeed + transform.rotation.eulerAngles.y, 0f);

    //     if(moveZPos != 0f)
    //     {
    //         //TODO: When fish catching added return if Reel and FishCaught are true so 
    //         //Reel animation can't be stopped by player movement if a fish is being caught
    //         IsFishingCheck();
            
    //         animator.SetBool(isWalkingAnim, true);
    //         transform.Translate(0f, 0f, moveZPos);
    //     }
    //     else
    //     {
    //         animator.SetBool(isWalkingAnim, false);
    //     }
    // }





    //    void Move()
    // {
    //     moveXPos = moveInput.x * turnSpeed * Time.deltaTime;
    //     moveZPos = moveInput.y * moveSpeed * Time.deltaTime;

    //     if(moveXPos != 0f || moveZPos != 0f)
    //     {
    //         //TODO: When fish catching added return if Reel and FishCaught are true so 
    //         //Reel animation can't be stopped by player movement if a fish is being caught
    //         IsFishingCheck();
            
    //         animator.SetBool(isWalkingAnim, true);
    //         transform.Translate(0f, 0f, moveZPos);
    //         transform.Rotate(0f, moveXPos, 0f, Space.Self);
    //     }
    //     else
    //     {
    //         animator.SetBool(isWalkingAnim, false);
    //     }
    // }