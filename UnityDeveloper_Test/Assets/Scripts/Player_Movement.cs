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

    public float checkGroundTimer;
    public float checkGroundRayDistance;

    private Rigidbody rb;
    public float CameraAngleOverride = 0.0f;

    Vector3 movementDirection;

    float timer = 0;
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
        SpeedControl();
        isGrounded = GroundCheck();
        if (!isGrounded)
        {
            timer += Time.deltaTime;
            if (timer > checkGroundTimer)
                anygroundBelow();
        }
        else
            timer = 0;
    }

    private void moov() //Moov player on Keyboard input
    {
        if (!isGrounded) return;
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
    }

    void SpeedControl() //Limits player's speed to constant
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump() //Perform Jump on space bar
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Space");
            rb.AddForce(transform.up * jumpForce, ForceMode.Force);
        }
    }

    private void Animation() //Control movement and air animation
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

    void anygroundBelow() //player will fall on ground or not
    {
        Ray anyGround = new Ray(transform.position + transform.up, -transform.up);

        // Perform the raycast
        if (Physics.Raycast(anyGround, out RaycastHit hit, checkGroundRayDistance))
            timer = 0;
        
        else
            GameManager.Instance.GameOver(false);
    }
}