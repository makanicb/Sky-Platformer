using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 20;
    public int playerHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TakeDamage(int damage) {
        playerHealth -= damage;
        healthBar.SetHealth(playerHealth);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("genericEnemy")) {
            TakeDamage(1);
            }
        }
}
