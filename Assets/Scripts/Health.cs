using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int maxPlayerHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update() {

        health = gameObject.GetComponent<PlayerController>().getHealth(); //QUICK FIX TO MAKE HEALTH WORK FOR ALPHA BUILD
                                                                           //TO-DO: REPLACE FUNCTIONALITY WITH OBSERVER

        if (health > maxPlayerHealth) {
            health = maxPlayerHealth;
        }
        
        for (int i = 0; i < hearts.Length; i++) {

            if (i < health) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < maxPlayerHealth) {
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }
    }

}
