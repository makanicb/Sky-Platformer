using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [HideInInspector]
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

    public int getHealth()
    {
        return this.health;
    }

    public int getMaxHealth()
    {
        return this.maxHealth;
    }

    public int setHealth(int health)
    {
        this.health = Mathf.Min(this.health + health, this.maxHealth);
        return this.health;
    }

    public int setMaxHealth(int maxHealth)
    {
        this.maxHealth = maxHealth;
        return this.maxHealth;
    }

}
