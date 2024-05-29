using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // Assign your prefab in the Inspector
    public float minSpawnInterval = 2f; // Minimum time between spawns
    public float maxSpawnInterval = 5f; // Maximum time between spawns
    public Collider islandBounds; // Assign the island's collider in the Inspector
    public Transform player; // Reference to the player

    private float spawnHeightOffset = 1.524f; // 5 feet in meters
    private bool playerInBounds = false;

    void Start()
    {
        // Ensure the islandBounds collider is a trigger
        if (islandBounds != null && !islandBounds.isTrigger)
        {
            islandBounds.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            playerInBounds = true;
            StartCoroutine(SpawnObjects());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInBounds = false;
            StopCoroutine(SpawnObjects());
        }
    }

    IEnumerator SpawnObjects()
    {
        while (playerInBounds)
        {
            SpawnObject();
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = GetRandomPositionWithinBounds();
        spawnPosition.y += spawnHeightOffset; // Adjust the y position
        Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomPositionWithinBounds()
    {
        Vector3 randomPosition;
        do
        {
            // Generate a random position within the island bounds
            float x = Random.Range(islandBounds.bounds.min.x, islandBounds.bounds.max.x);
            float z = Random.Range(islandBounds.bounds.min.z, islandBounds.bounds.max.z);
            randomPosition = new Vector3(x, islandBounds.transform.position.y, z);
        } while (!IsValidPosition(randomPosition));

        return randomPosition;
    }

    bool IsValidPosition(Vector3 position)
    {
        Ray ray = new Ray(position + Vector3.up * 10f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider == islandBounds;
        }
        return false;
    }
}