using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _RB;
    private Transform _TF;
    public float maxDistFromGround;
    public float hoverHeight;
    public float springStr;
    public float springDmp;
    // Start is called before the first frame update
    void Start()
    {
        _RB = gameObject.GetComponent<Rigidbody>();
        _TF = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        //Hover ("Making A Physics Based Character Controller in Unity" by Toyful Games. YouTube)
        RaycastHit hit;
        if(Physics.Raycast(_TF.position, _TF.TransformDirection(Vector3.down), out hit, maxDistFromGround))
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Vector3 vel = _RB.velocity;
            Vector3 rayDir = _TF.TransformDirection(Vector3.down);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitRB = hit.rigidbody;
            if(hitRB != null)
            {
                otherVel = hitRB.velocity;
            }

            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            float relVel = rayDirVel - otherDirVel;

            float x = hit.distance - hoverHeight;

            float springForce = (x * springStr) - (relVel * springDmp);

            _RB.AddForce(rayDir * springForce);
            Debug.Log("SF: " + springForce * rayDir);
        }
        else
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * maxDistFromGround, Color.red);
        }
    }
}
