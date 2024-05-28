using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public Transform orb; // Reference to the orb GameObject
    public float rotationSpeed = 10f; // Speed of rotation
    public Vector3 initialDirectionX = Vector3.right; // Initial direction for X-axis rotation
    public Vector3 initialDirectionY = Vector3.up; // Initial direction for Y-axis rotation
    public Vector3 initialDirectionZ = Vector3.forward; // Initial direction for Z-axis rotation

    void Update()
    {
        // Rotate around the specified direction for the Y-axis
        transform.RotateAround(orb.position, initialDirectionY, rotationSpeed * Time.deltaTime);

        // Rotate around the specified direction for the X-axis
        transform.RotateAround(orb.position, initialDirectionX, rotationSpeed * Time.deltaTime);

        // Rotate around the specified direction for the Z-axis
        transform.RotateAround(orb.position, initialDirectionZ, rotationSpeed * Time.deltaTime);
    }
}