using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public GameObject child;

    public float horizontalInputMultiplier;

    bool isGrounded;
    Animator animator;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // Adjust this to control the player rotation speed.
    public float jumpForce;
    public float groundCheckDistance;

    private Rigidbody rb;
    public float CameraAngleOverride = 0.0f;

    Vector3 movementDirection;

    void Start()
    {
        animator = child.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        moov();
        Jump();
        Animation();
        isGrounded = GroundCheck();
    }

    private void moov()
    {
        // Get input axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        movementDirection = (child.transform.forward * verticalInput + horizontalInput * horizontalInputMultiplier * child.transform.right).normalized;

        rb.AddForce(movementDirection * moveSpeed, ForceMode.Force);
        if (horizontalInput != 0f)
        {
            float rotationAmount = horizontalInput * rotationSpeed * Time.deltaTime;

            // Apply the rotation to the GameObject around the Y-axis
            child.transform.Rotate(0f, rotationAmount, 0f);
        }

        // Apply the movement as a force to the Rigidbody.
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space");
            rb.AddForce(transform.up * jumpForce, ForceMode.Force);
        }
    }

    private void Animation()
    {
        if (movementDirection != Vector3.zero)
        {
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        if (!isGrounded)
        {
            animator.SetBool("Ground", true);
        }
        else if (isGrounded)
        {
            animator.SetBool("Ground", false);
        }
    }

    bool GroundCheck()
    {
        // Cast a ray from the player's position towards the ground
        Ray groundRay = new Ray(transform.position + transform.up, -transform.up);

        Debug.DrawRay(groundRay.origin, groundRay.direction * groundCheckDistance, Color.yellow);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * groundCheckDistance, Color.yellow);

        // Perform the raycast
        if (Physics.Raycast(groundRay, out RaycastHit hit, groundCheckDistance))
        {
            // Check if the hit object has the "Ground" tag (adjust as needed)
            if (hit.collider.CompareTag("Ground"))
            {
                return true; // Player is grounded
            }
        }

        return false; // Player is not grounded
    }
}