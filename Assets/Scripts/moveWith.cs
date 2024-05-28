using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformFollower : MonoBehaviour
{
    private Transform movingPlatform;
    private Vector3 platformLastPosition;
    private bool onPlatform = false;

    private PlayerController playerController;

    void Start()
    {
        // Attempt to get the PlayerController component
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on the GameObject: " + gameObject.name);
        }
        else
        {
            Debug.Log("PlayerController component successfully found on the GameObject: " + gameObject.name);
        }
    }

    void Update()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController component is null in Update.");
            return;
        }

        // Check if the player is standing on a platform using the raycast hit information
        if (playerController.hit.collider != null)
        {
            Debug.Log("Hit collider found: " + playerController.hit.collider.name);
            if (playerController.hit.collider.CompareTag("moving"))
            {
                if (!onPlatform)
                {
                    movingPlatform = playerController.hit.transform;
                    platformLastPosition = movingPlatform.position;
                    onPlatform = true;
                }
                else
                {
                    // Adjust position based on platform movement
                    Vector3 platformMovement = movingPlatform.position - platformLastPosition;
                    transform.position += platformMovement;
                    platformLastPosition = movingPlatform.position;
                }
            }
            else
            {
                Debug.Log("Hit object is not a MovingPlatform.");
            }
        }
        else
        {
            Debug.Log("No hit collider found.");
            onPlatform = false;
            movingPlatform = null;
        }
    }
}