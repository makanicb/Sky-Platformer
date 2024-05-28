using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformFollower : MonoBehaviour
{
    private Transform movingPlatform;
    private Vector3 platformLastPosition;
    private bool onPlatform = false;

    void Update()
    {
        if (onPlatform && movingPlatform != null)
        {
            // Adjust position based on platform movement
            Vector3 platformMovement = movingPlatform.position - platformLastPosition;
            transform.position += platformMovement;
            platformLastPosition = movingPlatform.position;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            movingPlatform = collision.transform;
            platformLastPosition = movingPlatform.position;
            onPlatform = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            platformLastPosition = movingPlatform.position;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("moving"))
        {
            onPlatform = false;
            movingPlatform = null;
        }
    }
}