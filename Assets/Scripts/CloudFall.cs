using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFall : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }
}
