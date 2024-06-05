using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab; // Reference to the projectile prefab
    public float shootInterval = 2f;    // Time interval between shots
    public Transform shootPoint;        // Point from where the projectiles will be shot

    private Transform player;           // Reference to the player
    private float shootTimer;           // Timer to track shooting interval

    void Start()
    {
        player = GameObject.Find("Player").transform;
        Debug.Log("Getting player transform:" + player);
        shootTimer = shootInterval;
    }

    void Update()
    {
        // Update the shoot timer
        shootTimer -= Time.deltaTime;

        // Check if it's time to shoot
        if (shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        // Instantiate the projectile at the shoot point
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Set the projectile's target to the player
        BossProjectile projScript = projectile.GetComponent<BossProjectile>();
        if (projScript != null)
        {
            projScript.SetTarget(player);
        }
    }
}