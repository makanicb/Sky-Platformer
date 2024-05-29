using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform orbitCenter; // The center around which the object will orbit
    public float orbitSpeed = 1f; // Speed of the orbit
    public float orbitRadius = 2f; // Radius of the orbit
    public bool clockwise = true; // Direction of the orbit

    private void Update()
    {
        // Calculate the target position of the object in the orbit
        Vector3 orbitPosition = CalculateOrbitPosition();

        // Move the object to the target position
        transform.position = orbitPosition;

        // Calculate the direction from the object to the orbit center
        Vector3 lookDir = orbitCenter.position - transform.position;

        // Rotate the object to face the center of the orbit with the red axis
        transform.rotation = Quaternion.LookRotation(lookDir, transform.up);
    }

    private Vector3 CalculateOrbitPosition()
    {
        // Calculate the angle based on time, orbit speed, and direction
        float angle = Time.time * orbitSpeed * (clockwise ? 1 : -1);

        // Calculate the position in the orbit using trigonometry
        float x = orbitCenter.position.x + Mathf.Cos(angle) * orbitRadius;
        float z = orbitCenter.position.z + Mathf.Sin(angle) * orbitRadius;
        float y = orbitCenter.position.y; // You can adjust this if you want the orbit to be inclined

        return new Vector3(x, y, z);
    }
}
