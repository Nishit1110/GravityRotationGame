using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEnvironment : MonoBehaviour
{
    public float rotationSpeed = 10f;
    private Vector3 gravityDirection = Vector3.down;

    // Reference to the player
    public Transform player;

    void Update()
    {
        gravityDirection = Physics.gravity;
        RotateEnvironmentAroundPlayer(gravityDirection);
    }

    void RotateEnvironmentAroundPlayer(Vector3 targetGravityDirection)
    {
        // Calculate the rotation needed to align the current up direction with the target gravity direction
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetGravityDirection);

        // Rotate the environment around the player
        transform.RotateAround(player.position, targetGravityDirection, rotationSpeed * Time.deltaTime);
    }
}