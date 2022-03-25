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

    float moveXPos;
    float MoveZPos;
    string isWalking;

    void Start()
    {
        isWalking = "IsWalking";

        animator = GetComponent<Animator>();
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
            animator.SetBool(isWalking, true);
            transform.Translate(0f, 0f, MoveZPos);
            transform.Rotate(0f, moveXPos, 0f, Space.Self);
        }
        else
        {
            animator.SetBool(isWalking, false);
        }
    }


    void OnMove(InputValue Value)
    {
        moveInput = Value.Get<Vector2>();
        Debug.Log(moveInput);
    }
}
