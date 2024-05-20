using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dameagable : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public void damage(int dmg)
    {
        health -= dmg;
        health = Mathf.Min(health, maxHealth);
        if (health == 0)
            onDeath();
    }

    protected virtual void onDeath()
    {
        gameObject.SetActive(false);
    }

}
