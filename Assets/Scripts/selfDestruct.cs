using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestruct : MonoBehaviour
{
    public float minLifetime = 3f; // Minimum time before self-destruct
    public float maxLifetime = 10f; // Maximum time before self-destruct

    void Start()
    {
        StartCoroutine(DestroyAfterRandomTime());
    }

    IEnumerator DestroyAfterRandomTime()
    {
        float lifetime = Random.Range(minLifetime, maxLifetime);
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
