using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    [Header("Controls")]
    public KeyCode jumpkey = KeyCode.Space;
    public float mouseSens;

    [Header("GroundCheck")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.7f + 0.3f, whatIsGround);
        // Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.7f + 0.3f), Color.blue, 0.1f);

        Inputs();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(jumpkey) && grounded)
        {
            Jump();
        }
    }

    private void PlayerMovement()
    {
        Vector3 mouseDirection = GetMouseDirection();

        if (grounded)
            rb.AddForce(mouseDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(mouseDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * mouseSens * Time.deltaTime);
    }

    private Vector3 GetMouseDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.WorldToScreenPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.y));
        return (worldMousePosition - transform.position).normalized;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        // Jump cooldown logic if needed
    }
}
