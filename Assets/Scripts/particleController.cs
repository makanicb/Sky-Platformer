using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLifetimeLimit : MonoBehaviour
{
    public float maxLifetime = 1.0f; // Maximum lifetime of a particle

    private ParticleSystem ps; // Reference to the Particle System

    void Start()
    {
        ps = GetComponent<ParticleSystem>(); // Get the ParticleSystem component
    }

    void Update()
    {
        // Check if the particle system is alive
        if (!ps.IsAlive())
        {
            // If the particle system is not alive, destroy the GameObject
            Destroy(gameObject);
        }
    }
}