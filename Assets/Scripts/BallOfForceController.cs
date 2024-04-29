using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfForceController : MonoBehaviour
{
    public float explosionForce;
    public float upwardMod;
    private Transform _TF;
    private float explosionRadius;
    // Start is called before the first frame update
    void Start()
    {
        _TF = gameObject.GetComponent<Transform>();
        explosionRadius = gameObject.GetComponent<SphereCollider>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EXPLOSION");
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.AddExplosionForce(explosionForce, _TF.position, Mathf.Infinity, upwardMod, ForceMode.Impulse);
    }
}
