using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    InputManager inputManager;
    AnimatorManager animatorManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRb;

    #region Movement Variables
    [Header("Falling")]
    public float inAirTimer;
    //public float leapingVelocity;
    public float fallingVelocity;
    public float rayCastHeightOffSet = 0.1f;
    public LayerMask whatIsGround;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    //public bool isJumping;
    public bool wantsJump;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    ///public float runningSpeed = 5;
    public float sprintingSpeed = 5;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;
    #endregion

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        animatorManager = GetComponent<AnimatorManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, rayCastHeightOffSet, whatIsGround);
        Debug.DrawRay(transform.position, Vector3.down * (rayCastHeightOffSet), Color.blue, 0.1f);
    }

    public void handleAllMovement()
    {
        HandleFallingAndLanding();
        HandleMovement();
        HandleRotation();
        //HandleJump();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;



        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }


        //moveDirection = moveDirection * walkingSpeed;  

        //Vector3 movementVelocity = moveDirection;
        playerRb.velocity = moveDirection;

    }

    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();

        targetDirection.y = 0;


        if (targetDirection == Vector3.zero) 
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        //RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (!isGrounded && !wantsJump)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            //playerRb.AddForce(transform.forward * leapingVelocity);
            playerRb.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }
            if (isGrounded )
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }
            inAirTimer = 0;
            playerManager.isInteracting = false;
            //wantsJump = false; // Reset wantsJump when landing

    }


    public void HandleJump()
    {
        if (isGrounded && wantsJump)
        {
            //animatorManager.animator.SetBool("wantsJump", true);
            //animatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);


            moveDirection.y = jumpingVelocity;
            playerRb.velocity = moveDirection;
        }
        
    }


}
