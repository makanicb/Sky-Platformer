using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dashes : MonoBehaviour
{
    public int dashes;
    public int maxDashes;
    public PlayerController player;

    public Image[] charges;
    //public Sprite fullCharge;
    //public Sprite emptyCharge;

    void Update() {

        dashes = player.getDashes();

        if (dashes > maxDashes) {
            dashes = maxDashes;
        }
        
        for (int i = 0; i < charges.Length; i++) {

            /*if (i < dashes) {
                charges[i].sprite = fullCharge;
            } else {
                charges[i].sprite = emptyCharge;
            }*/

            if (i < maxDashes && i < dashes) {
                charges[i].enabled = true;
            } else {
                charges[i].enabled = false;
            }
        }
    }

}
