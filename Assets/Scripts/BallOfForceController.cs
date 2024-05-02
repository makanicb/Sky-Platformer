using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOfForceController : MonoBehaviour
{
    public float explosionForce;
    public float upwardMod;
    private Transform _TF;
    public Rigidbody _PRB;
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
        //Debug.Log("EXPLOSION");
        Rigidbody rb = other.attachedRigidbody;
        Transform tf = other.transform;
        if (rb != null && !_TF.IsChildOf(tf))
        {
            Vector3 offset = tf.position - _TF.position;
            Vector3 dir = offset.normalized;
            Vector3 velDir = _PRB.velocity.normalized;
            //Debug.Log("dir " + dir);
            //Debug.Log("vel " + velDir);
            float expForce = Mathf.Max(0f, explosionForce * Vector3.Dot(velDir, dir));
            //Debug.Log(expForce);
            rb.AddExplosionForce(expForce, _TF.position, Mathf.Infinity, upwardMod, ForceMode.Impulse);
        }
        //Debug.Log(other.gameObject.name);
        if(tf.gameObject.CompareTag("genericEnemy"))
        {
            EnemyController enemyController = tf.gameObject.GetComponent<EnemyController>();
            //Debug.Log(enemyController);
            if(enemyController != null)
            {
                //Debug.Log("Hurt enemy through dash");
                enemyController.health--;
                Debug.Log("Remaining health " + enemyController.health);
                if(enemyController.health == 0)
                {
                    other.gameObject.SetActive(false);
                }
            }
        }

    }
}
