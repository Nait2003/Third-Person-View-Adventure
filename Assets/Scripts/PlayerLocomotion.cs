using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Unity.Mathematics;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRb;

    public bool isSprinting;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    ///public float runningSpeed = 5;
    public float sprintingSpeed = 5;
    public float rotationSpeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRb = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void handleAllMovement()
    {
        HandleMovement();
        HandleRotation();
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


        moveDirection = moveDirection * walkingSpeed;  

        Vector3 movementVelocity = moveDirection;
        playerRb.velocity = movementVelocity;

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
}
