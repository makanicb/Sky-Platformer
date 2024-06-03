using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 5f;            // Speed of the projectile
    public float rotateSpeed = 200f;    // Rotation speed for tracking
    private Transform target;           // Reference to the target (player)

    void Update()
    {
        if (target != null)
        {
            // Direction to the target
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            // Calculate rotation step
            float rotateStep = rotateSpeed * Time.deltaTime;

            // Calculate new direction after rotation
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotateStep, 0.0f);

            // Apply rotation
            transform.rotation = Quaternion.LookRotation(newDirection);

            // Move the projectile forward
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            // Destroy the projectile if there is no target
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle collision with player (e.g., apply damage)
            Debug.Log("Hit Player!");

            // Destroy the projectile on impact
            Destroy(gameObject);
        }
    }
}
