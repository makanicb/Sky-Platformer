using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectUp : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Speed at which the object moves up
    public float distanceToMove = 700.0f; // Distance to move the object upwards

    private bool isMoving = false; // Flag to track if the object is moving
    private float initialYPosition; // Initial position of the object on the Y-axis

    void Start()
    {
        initialYPosition = transform.position.y; // Store the initial Y position of the object
        isMoving = true; // Start moving the object
    }

    void Update()
    {
        if (isMoving)
        {
            // Calculate the new position based on the current position and the move speed
            float newYPosition = transform.position.y + moveSpeed * Time.deltaTime;

            // Move the object upwards
            transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

            // Check if the object has moved the specified distance
            if (transform.position.y - initialYPosition >= distanceToMove)
            {
                isMoving = false; // Stop moving the object once it reaches the desired distance
            }
        }
    }
}
