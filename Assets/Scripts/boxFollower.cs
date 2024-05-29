using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonkeyFollowBox : MonoBehaviour
{
    public Transform box; // Reference to the box's transform
    private Vector3 initialOffset; // Initial offset between the monkey head and the box

    void Start()
    {
        if (box != null)
        {
            // Calculate the initial offset between the monkey head and the box
            initialOffset = transform.position - box.position;

        }
        else
        {
            Debug.LogError("Box Transform reference is not set.");
        }
    }

    void LateUpdate()
    {
        if (box != null)
        {
            // Update the position of the monkey head to follow the box's position
            transform.position = box.position + initialOffset;
        }
    }

    
}