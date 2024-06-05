using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float destroyTime = 15; // Time until destroyed

    void Start()
    {
        // Schedule the destruction of the object
        Destroy(gameObject, destroyTime);
    }
}
