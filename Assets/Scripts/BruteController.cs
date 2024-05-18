using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruteController : MonoBehaviour
{
    // Initialize vars
    public GameObject brute;
    public GameObject player;
    public int health;
    float bruteSpeed;
    float minDist;
    float randX;
    float randZ;
    Vector3 randDir;
    float idleTimer;

    enum State
    {
        Passive,
        Aggressive
    };
    State state;

    Coroutine idleCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("These logs can be commented out in EnemyController.cs");
        health = 7;
        Debug.Log("Starting health: " + health);

        player = GameObject.Find("PlayerManager/Player");
        bruteSpeed = 3f;
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
        Vector3 brutePosition = new Vector3(brute.transform.position.x, 0f, brute.transform.position.z);
        Vector3 playerPosition = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        Vector3 direction = (playerPosition - brutePosition).normalized;

        switch (state)
        {
            case State.Passive: // Idle movement
                // Look in direction of movement
                Vector3 lookIdlePosition = new Vector3(brute.transform.position.x + randDir.x,
                    brute.transform.position.y, brute.transform.position.z + randDir.z);
                brute.transform.LookAt(lookIdlePosition);

                // Idle movement
                brute.transform.position += randDir.normalized * bruteSpeed * Time.fixedDeltaTime * 0.25f;
                break;

            case State.Aggressive: // Chase player
                // Look at the player
                Vector3 lookAtPosition = new Vector3(player.transform.position.x,
                    brute.transform.position.y, player.transform.position.z);
                brute.transform.LookAt(lookAtPosition);

                // Calculate distance between enemy and player
                float distance = Vector3.Distance(brutePosition, playerPosition);

                // Move towards player if distance is greater than minDist
                if (distance > minDist)
                {
                    brute.transform.position += direction * bruteSpeed * Time.fixedDeltaTime;
                }
                // Stop moving if distance is less than or equal to minDist
                else
                {
                    brute.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                break;

            default:
                Debug.Log("Enemy isn't in a state. How did you get here?");
                break;
        }
    }

    // Decrement health and destroy usedItem if it hits enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("usedItem"))
        {
            // Decrement health
            health--;
            Debug.Log("Remaining health: " + health);

            // Change state
            if (state == State.Passive)
            {
                state = State.Aggressive;
            }

            // Since tag is on child of prefab, get parent to access prefab
            var parentObject = other.transform.parent.gameObject;
            parentObject.SetActive(false);

            // Destroy enemy if health drops to 0
            if (health <= 0)
            {
                // Debug.Log("You killed the enemy!");
                brute.SetActive(false);
            }
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
}
