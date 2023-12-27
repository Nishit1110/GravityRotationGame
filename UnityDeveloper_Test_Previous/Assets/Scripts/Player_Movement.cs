using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public GameObject child;

    bool isGrounded;
    Animator animator;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f; // Adjust this to control the player rotation speed.
    public float jumpForce;
    public float groundCheckDistance;

    private Rigidbody rb;
    private Transform mainCameraTransform;

    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    public float CameraAngleOverride = 0.0f;

    Vector3 movementDirection;

    void Start()
    {
        animator = child.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //CameraMoov();
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

        movementDirection = (child.transform.forward * verticalInput + child.transform.right * horizontalInput).normalized;

        rb.AddForce(movementDirection * moveSpeed, ForceMode.Force);
        if (horizontalInput != 0f)
        {
            /*Debug.Log(horizontalInput);
            Quaternion toRotation = Quaternion.Euler(0, child.transform.eulerAngles.y + horizontalInput * rotationSpeed, 0);
            child.transform.rotation = Quaternion.Slerp(child.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        */
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

   /* private void CameraMoov()
    {
        // Get the mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // if there is an input and camera position is not fixed
        if (Mathf.Abs(mouseX) >= _threshold)
        {
            // Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetYaw += mouseX * deltaTimeMultiplier;
        }

        if (Mathf.Abs(mouseY) >= _threshold)
        {
            // Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier = 1.0f;

            _cinemachineTargetPitch += mouseY * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp + transform.eulerAngles.x, TopClamp + transform.eulerAngles.x);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }*/
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