using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : MonoBehaviour
{
    public GameObject pickup;

    // Start is called before the first frame update
    void Start()
    {
        pickup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter (Collider other) {
        if(other.gameObject.CompareTag("PickUp")){
        pick();
        }
    }

    void pick()
    {
        pickup.SetActive(true);
    }

    void drop()
    {
        pickup.SetActive(false);
    }

    void OnUse()
    {
        drop();
    }
    
}
