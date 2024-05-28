using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EyeLookAtPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform

    void Update()
    {
        if (player != null)
        {
            // Make the eye model look at the player
            transform.LookAt(player);
        }
    }
}
