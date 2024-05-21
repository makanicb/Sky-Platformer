using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.UI;
using UnityEngine;
//using static UnityEditor.Progress;

public class EnemyController : Damageable
{
    // Initialize vars
    public GameObject enemy;
    public GameObject player;
    //public int health;
    float enemySpeed;
    float minDist;
    float randX;
    float randZ;
    Vector3 randDir;
    float idleTimer;

    enum State
    {
        Passive,
        Aggressive,
        Flee
    };
    State state;

    Coroutine idleCoroutine;
    Coroutine fleeCoroutine;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        Debug.Log("These logs can be commented out in EnemyController.cs");
        health = 3;
        Debug.Log("Starting health: " + health);

        player = GameObject.Find("PlayerManager/Player");
        enemySpeed = 4f;
        minDist = 4f;
        idleTimer = 3f;
        state = State.Passive;

        randX = Random.Range(-100, 101);
        randZ = Random.Range(-100, 101);
        randDir = new Vector3(randX, 0f, randZ).normalized;
        idleCoroutine = StartCoroutine(IdleTimer(idleTimer));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Declare common variables
        Vector3 enemyPosition = new Vector3(enemy.transform.position.x, 0f, enemy.transform.position.z);
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        Vector3 direction = (playerPosition - enemyPosition).normalized;

        switch (state)
        {
            case State.Passive: // Idle movement
                // Look in direction of movement
                Vector3 lookIdlePosition = new Vector3(enemy.transform.position.x + randDir.x,
                    enemy.transform.position.y, enemy.transform.position.z + randDir.z);
                enemy.transform.LookAt(lookIdlePosition);

                // Idle movement
                enemy.transform.position += randDir.normalized * enemySpeed * Time.fixedDeltaTime * 0.25f;
                break;

            case State.Aggressive: // Chase player
                // Look at the player
                Vector3 lookAtPosition = new Vector3(player.transform.position.x,
                    enemy.transform.position.y, player.transform.position.z);
                enemy.transform.LookAt(lookAtPosition);

                // Calculate distance between enemy and player
                float distance = Vector3.Distance(enemyPosition, playerPosition);

                // Move towards player if distance is greater than minDist
                if (distance > minDist)
                {
                    enemy.transform.position += direction * enemySpeed * Time.fixedDeltaTime;
                }
                // Stop moving if distance is less than or equal to minDist
                else
                {
                    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                break;

            case State.Flee: // Run from player
                // Look away from the player
                Vector3 lookAwayPosition = new Vector3(enemy.transform.position.x - direction.x, 
                    enemy.transform.position.y, enemy.transform.position.z - direction.z);
                enemy.transform.LookAt(lookAwayPosition);

                // Run away from the player
                enemy.transform.position += direction * enemySpeed * Time.fixedDeltaTime * -1;
                break;

            default:
                Debug.Log("Enemy isn't in a state. How did you get here?");
                break;
        }
    }

    // Decrement health and destroy usedItem if it hits enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Damager"))
        {
            // Decrement health
            Debug.Log("Remaining health: " + health);

            // Change state
            if (state == State.Passive)
            {
                state = State.Aggressive;
            }
            else if (health == 1)
            {
                state = State.Flee;
                fleeCoroutine = StartCoroutine(FleeTimer(5f));
            }

            // Since tag is on child of prefab, get parent to access prefab
            /*var parentObject = other.transform.parent.gameObject;
            parentObject.SetActive(false);*/

            // Destroy enemy if health drops to 0
            /*if (health <= 0)
            {
                // Debug.Log("You killed the enemy!");
                enemy.SetActive(false);
            }*/
        }
    }

    IEnumerator IdleTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        // After delay, reset the idle direction
        randX = Random.Range(-100, 101);
        randZ = Random.Range(-100, 101);
        randDir = new Vector3(randX, 0f, randZ).normalized;
        idleCoroutine = StartCoroutine(IdleTimer(delay));
    }

    IEnumerator FleeTimer(float delay)
    {
        yield return new WaitForSeconds(delay);
        // After delay, set the state back to aggressive
        state = State.Aggressive;
    }
}
