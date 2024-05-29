using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public Collider detectionBounds; // Assign the detection collider in the Inspector
    public float minDestroyInterval = 2f; // Minimum time before object destruction
    public float maxDestroyInterval = 5f; // Maximum time before object destruction

    void Start()
    {
        // Ensure the detectionBounds collider is a trigger
        if (detectionBounds != null && !detectionBounds.isTrigger)
        {
            detectionBounds.isTrigger = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Start the destruction process for the detected object
        StartCoroutine(DestroyObjectAfterInterval(other.gameObject));
    }

    IEnumerator DestroyObjectAfterInterval(GameObject obj)
    {
        float destroyInterval = UnityEngine.Random.Range(minDestroyInterval, maxDestroyInterval);
        yield return new WaitForSeconds(destroyInterval);
        Destroy(obj);
    }
}
