using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _RB;
    private Transform _TF;
    private Transform _LF;
    
    //Control hover
    public float maxDistFromGround;
    public float hoverHeight;
    public float springStr;
    public float springDmp;
    
    //Player movement
    private Vector3 wishDir;
    public float MAX_SPEED;
    public float MAX_ACCEL;
    public float MAX_FRICTION;
    public float friction_coef;
    public float MAX_DRAG;
    public float drag_coef;
    private float workingMaxFriction;
    private float workingFrictionCoef;
    
    //Jumping
    private bool wishJump;
    private bool grounded;
    public float jumpStr;
    private bool falling;

    // Player health
    public int playerHealth;

    // Holding Item
    private bool holdingItem;
    public GameObject heldItem;
    public Rigidbody usedItem;
    public LaunchProjectile lp;

    //Dashing
    public int maxDashes;
    public float rechargeTime;
    public float dashSpeed;
    private int dashes;
    private float timeSinceDash;
    private bool wishDash;

    //Charging
    private GameObject BoF;
    public float chargeThreshold;

    //ResetScene
    public ResetController reset;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize player controller
        _RB = gameObject.GetComponent<Rigidbody>();
        _TF = gameObject.GetComponent<Transform>();
        _LF = _TF.Find("LookPoint");
        BoF = _TF.Find("BallOfForce").gameObject;

        wishDir = new Vector3(0, 0, 0);
        //wishDirR = new Vector3(0, 0, 0);
        wishJump = false;
        grounded = false;
        falling = true;
        workingMaxFriction = MAX_FRICTION;
        workingFrictionCoef = friction_coef;
        dashes = maxDashes;
        timeSinceDash = 0;
        wishDash = false;

        // Player health
        playerHealth = 3;

        // Initialize Item related things
        holdingItem = false;
        heldItem = GameObject.Find("Player/heldItem");
        heldItem.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //Debug.Log(falling);
        Vector3 vel = _RB.velocity;

        //Charging?
        //Debug.Log(vel.magnitude + ">=" + chargeThreshold);
        //Debug.Log(vel.magnitude >= chargeThreshold);
        BoF.SetActive((vel.magnitude >= chargeThreshold));

        if(!falling && vel.y <= 0)
        {
            falling = true;
        }
        //Hover ("Making A Physics Based Character Controller in Unity" by Toyful Games. YouTube)
        RaycastHit hit;
        if (Physics.Raycast(_TF.position, _TF.TransformDirection(Vector3.down), out hit, maxDistFromGround))
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Vector3 rayDir = _TF.TransformDirection(Vector3.down);
            if (falling)
            {
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
                grounded = true;
            }
        }
        else
        {
            Debug.DrawRay(_TF.position, _TF.TransformDirection(Vector3.down) * maxDistFromGround, Color.red);
            workingMaxFriction = MAX_DRAG;
            workingFrictionCoef = drag_coef;
            grounded = false;
        }

        //Jump
        if(grounded && wishJump)
        {
            _RB.AddForce(_TF.up * jumpStr, ForceMode.VelocityChange);
            falling = false;
            grounded = false;
            workingMaxFriction = MAX_DRAG;
            workingFrictionCoef = drag_coef;
        }
        wishJump = false;

        //Dash
        if (dashes > 0 && wishDash)
        {
            dashes--;
            Vector3 dashDir = wishDir.magnitude == 0 ? _LF.forward : _LF.rotation * wishDir;
            dashDir *= dashSpeed;
            _RB.AddForce(dashDir, ForceMode.VelocityChange);
            timeSinceDash = 0;
        }
        else if (dashes < maxDashes && timeSinceDash > rechargeTime * (dashes + 1))
        {
            dashes++;
        }
        timeSinceDash += Time.deltaTime;
        wishDash = false;

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

    void OnJump()
    {
        wishJump = true;
    }

    void OnDash()
    {
        wishDash = true;
    }

    // For Generic Collectible
    void OnTriggerEnter(Collider other)
    {
        // For Generic Collectible
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            holdingItem = true;
            heldItem.SetActive(true);
        }

        // For Generic Enemy
        if (other.gameObject.CompareTag("genericEnemy"))
        {
            playerHealth--;
            Debug.Log("Ouch! You're down to " + playerHealth + " health.");

            // Do something if health drops to 0
            if (playerHealth <= 0)
            {
                // Do a thing
                Debug.Log("YOU DIED! HAHAHAHA");
                reset.ResetScene();
            }
        }
    }

    void OnUse()
    {
        // F to use item
        // This will need revisiting if we want to support button remapping
        if (holdingItem == true)
        {
            heldItem.SetActive(false);
            holdingItem = false;
            lp.Launch();
        }
    }

}
