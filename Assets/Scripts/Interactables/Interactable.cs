using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	public float radius = 3f;


	void Update ()
	{
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.tag == "Player") 
            {
                // Player is within radius, interact with the object
                Interact();
                break; // Exit the loop after interacting with the first player
            }
        }
	}
	
	void Start()
    {
        // Add a trigger collider to the object
        gameObject.AddComponent<BoxCollider>().isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Player entered the trigger area, interact with the object
            Interact();
        }
    }


	// This method is meant to be overwritten
	public virtual void Interact ()
	{
		Debug.Log("Item picked up!");
	}



}

