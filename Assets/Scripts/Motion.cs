using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBackAndForth : MonoBehaviour
{
    public Vector3 direction = Vector3.right; // Direction of movement
    public float distance = 5f; // Total distance to move
    public float speed = 2f; // Speed of movement

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the movement based on the direction, distance, and speed
        float movement = Mathf.PingPong(Time.time * speed, distance) - (distance / 2);

        // Calculate the position offset based on the movement and direction
        Vector3 offset = direction.normalized * movement;

        // Update object's position
        transform.position = startPosition + offset;
    }
}