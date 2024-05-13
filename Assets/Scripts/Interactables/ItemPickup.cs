using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Pickup : MonoBehaviour
{
    public Item item;

	public List<Item> items = new List<Item>();
 
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && (items.Count < 6))
        {
            Inventory.instance.Add(item);
			Debug.Log("Picking up " + item.name);
            Destroy(gameObject);
        }
		
	}
}
