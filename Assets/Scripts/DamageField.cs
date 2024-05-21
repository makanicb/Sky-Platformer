using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageField : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private List<string> targets;

    
    protected virtual void OnTriggerEnter(Collider other)
    {
        foreach(string t in targets)
        {
            if(other.gameObject.CompareTag(t))
            {
                Damageable dam = other.GetComponent<Damageable>();
                if(dam is not null)
                {
                    dam.damage(damage);
                }
            }
        }
    }
}
