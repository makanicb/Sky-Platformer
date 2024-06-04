using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartController : MonoBehaviour
{
    public int healthIncreaseAmount = 1; // Amount to increase max health by, default is 1

    void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Get the PlayerController component from the collided object
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                // Increase the player's max health
                playerController.maxHealth += healthIncreaseAmount;
                playerController.health += healthIncreaseAmount;
                
                // Destroy the heart collectible
                Destroy(gameObject);
            }
        }
    }
}