using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    [Header("Target Objects")]
    public Transform targetTransform; //The camera follow object
    public Transform cameraPivot;     //The object the camera uses to pivot (look up/down)
    public Transform cameraTransform; //The Transform of the camera object in the scene
    public LayerMask collisionLayers;  //The layers the camera will collide with
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private Vector3 cameraVectorPosition;
    private float defaultPosition;

    [Header("References")]
    public float cameraCollisionOffset = 0.2f; //how much the camera will jump off of objects its colliding with
    public float minimumCollisionOffset = 0.2f;
    public float cameraCollisionRadius = 0.2f;
    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 15f;
    public float cameraPivotSpeed = 15f;
    public float camLookSmoothTime = 1f;

    public float lookAngle; // cam up/down
    public float pivotAngle; // cam left/right
    public float minimumPivotAngle = -35;
    public float maximumPivotAngle = 35;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        inputManager = FindObjectOfType<InputManager>();
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        cameraTransform = Camera.main.transform;
        defaultPosition = cameraTransform.localPosition.z;
    }

    public void HandleAllCameraMovement()
    {
        FollowTarget();
        RotateCamera();
        HandleCameraCollisions();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = Vector3.SmoothDamp
            (transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);

        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        Vector3 rotation;
        Quaternion targetRotation;

        lookAngle = Mathf.Lerp(lookAngle, lookAngle + (inputManager.camHorizontalInput * cameraLookSpeed), 
            camLookSmoothTime * Time.deltaTime);

        pivotAngle = Mathf.Lerp(pivotAngle, pivotAngle - (inputManager.camVerticalInput * cameraPivotSpeed), 
            camLookSmoothTime * Time.deltaTime);

        pivotAngle = Mathf.Clamp(pivotAngle, minimumPivotAngle, maximumPivotAngle);

        rotation = Vector3.zero;
        rotation.y = lookAngle;
        targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }

    private void HandleCameraCollisions()
    {
        float targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivot.position;
        direction.Normalize();

        if (Physics.SphereCast
            (cameraPivot.transform.position, cameraCollisionRadius, direction, out hit, Mathf.Abs(targetPosition), collisionLayers))
        {
            float distance = Vector3.Distance(cameraPivot.position, hit.point);
            targetPosition = - (distance - cameraCollisionOffset);
        }

        if(Mathf.Abs(targetPosition) < minimumCollisionOffset)
        {
            targetPosition = targetPosition - minimumCollisionOffset;
        }

        cameraVectorPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition, 0.2f);
        cameraTransform.localPosition = cameraVectorPosition;
    }
}
