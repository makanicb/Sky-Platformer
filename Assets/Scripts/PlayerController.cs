using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _RB;
    private Transform _TF;
    //Control hover
    public float maxDistFromGround;
    public float hoverHeight;
    public float springStr;
    public float springDmp;
    //Player movement
    private Vector3 wishDir;
    //private Vector3 wishDirR;
    public float MAX_SPEED;
    public float MAX_ACCEL;
    public float MAX_FRICTION;
    public float friction_coef;
    public float MAX_DRAG;
    public float drag_coef;
    // Start is called before the first frame update
    void Start()
    {
        _RB = gameObject.GetComponent<Rigidbody>();
        _TF = gameObject.GetComponent<Transform>();
        wishDir = new Vector3(0, 0, 0);
        //wishDirR = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        float workingMaxFriction, workingFrictionCoef;
        Vector3 vel = _RB.velocity;
        //Hover ("Making A Physics Based Character Controller in Unity" by Toyful Games. YouTube)
        RaycastHit hit;
        if (Physics.Raycast(_TF.position, _TF.TransformDirection(Vector3.down), out hit, maxDistFromGround))
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Vector3 rayDir = _TF.TransformDirection(Vector3.down);

            Vector3 otherVel = Vector3.zero;
            Rigidbody hitRB = hit.rigidbody;
            if (hitRB != null)
            {
                otherVel = hitRB.velocity;
            }

            float rayDirVel = Vector3.Dot(rayDir, vel);
            float otherDirVel = Vector3.Dot(rayDir, otherVel);

            float relVel = rayDirVel - otherDirVel;

            float x = hit.distance - hoverHeight;

            float springForce = (x * springStr) - (relVel * springDmp);

            _RB.AddForce(rayDir * springForce);
            //Debug.Log("SF: " + springForce * rayDir);
            workingMaxFriction = MAX_FRICTION;
            workingFrictionCoef = friction_coef;
        }
        else
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * maxDistFromGround, Color.red);
            workingMaxFriction = MAX_DRAG;
            workingFrictionCoef = drag_coef;
        }

        Vector3 wishDirT = _TF.rotation * wishDir;
        Vector3 hvel = new Vector3(vel.x, 0f, vel.z);
        //Move character
        float currentSpeed = Vector3.Dot(hvel, wishDirT);
        //If wishDir is 0, apply friction
        /*if (Vector3.Dot(wishDir, wishDir) == 0)
        {
            Vector3 friction = Vector3.ClampMagnitude(-hvel * friction_coef, MAX_FRICTION);
            _RB.AddForce(friction, ForceMode.Acceleration);
            Debug.Log("friction = " + friction);
        }*/
        /*else //Otherwise apply friction perpendicular to wishDir
        {
            float currentDrift = Vector3.Dot(hvel, wishDirR);
            Vector3 friction = Vector3.ClampMagnitude(-wishDirR * currentDrift * friction_coef,
                MAX_FRICTION);
            Debug.Log("friction = " + friction);
            _RB.AddForce(friction, ForceMode.Acceleration);
        }*/
        float addSpeed = Mathf.Clamp(MAX_SPEED - currentSpeed, 0f, MAX_ACCEL);
        //Debug.Log("addSpeed = " + addSpeed * wishDir);
        _RB.AddForce(addSpeed * wishDirT, ForceMode.Acceleration);
        //Friction always points towards wishDirT
        Vector3 driftVel = Mathf.Abs(currentSpeed) * wishDirT - hvel;
        Vector3 friction = Vector3.ClampMagnitude(driftVel * workingFrictionCoef, workingMaxFriction);
        //Debug.Log("friction = " + friction);
        _RB.AddForce(friction, ForceMode.Acceleration);
    }

    void OnMove(InputValue moveValue)
    {
        Vector2 move = moveValue.Get<Vector2>();
        wishDir.x = move.x;
        wishDir.z = move.y;
        /*//wishDirR is wishDir rotated 90 degrees to the right
        wishDirR.x = move.y;
        wishDirR.z = -move.x;*/
    }
}
