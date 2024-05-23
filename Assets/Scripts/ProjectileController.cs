using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : DamageField
{
    protected override void OnTriggerEnter(Collider other)
    {
        foreach (string t in targets)
        {
            if(other.gameObject.CompareTag(t))
            {
                Damageable dam = other.GetComponent<Damageable>();
                if(dam is not null)
                {
                    Debug.Log("BURN");
                    dam.damage(damage);
                    gameObject.transform.parent.gameObject.SetActive(false);
                }
                
            }
        }
    }
}
