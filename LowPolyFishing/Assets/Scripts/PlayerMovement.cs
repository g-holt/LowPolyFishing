using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float turnSpeed = 100f;    //Angle of Turn

    Vector2 moveInput;
    Animator animator;
    PlayerInput playerInput;

    float moveXPos;
    float MoveZPos;

    string reelAnim;
    string isWalkingAnim;
    string isFishingAnim;

    public bool isFishing;


    void Start()
    {
        reelAnim = "Reel";
        isWalkingAnim = "IsWalking";
        isFishingAnim = "IsFishing";

        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    
    void Update()
    {
        Move();
    }


    void Move()
    {
        moveXPos = moveInput.x * turnSpeed * Time.deltaTime;
        MoveZPos = moveInput.y * moveSpeed * Time.deltaTime;

        if(moveXPos != 0f || MoveZPos != 0f)
        {
            //TODO: When fish catching added return if Reel and FishCaught are true so 
            //Reel animation can't be stopped by player movement if a fish is being caught
            IsFishingCheck();
            
            animator.SetBool(isWalkingAnim, true);
            transform.Translate(0f, 0f, MoveZPos);
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
            playerInput.SwitchCurrentActionMap("Player");
            animator.SetBool(isFishingAnim, false);
            animator.SetBool(reelAnim, false);
        }
    }


    void OnMove(InputValue Value)
    {
        moveInput = Value.Get<Vector2>();
    }
    
}
