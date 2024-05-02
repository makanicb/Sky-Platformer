using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.UI;
using UnityEngine;
//using static UnityEditor.Progress;

public class EnemyController : MonoBehaviour
{
    // Initialize vars
    public GameObject enemy;
    public int health;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("These logs can be commented out in EnemyController.cs");
        health = 3;
        Debug.Log("Starting health: " + health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Decrement health and destroy usedItem if it hits enemy
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("usedItem"))
        {
            health--;
            Debug.Log("Remaining health: " + health);

            // Since tag is on child of prefab, get parent to access prefab
            var parentObject = other.transform.parent.gameObject;
            parentObject.SetActive(false);

            // Destroy enemy if health drops to 0
            if (health <= 0)
            {
                Debug.Log("You killed the enemy!");
                enemy.SetActive(false);
            }
        }
    }
}
