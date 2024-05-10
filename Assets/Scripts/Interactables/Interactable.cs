using UnityEngine;
using UnityEngine.AI;

// [RequireComponent(typeof(ColorOnHover))]
public class Interactable : MonoBehaviour {

	public float radius = 3f;
	public Transform interactionTransform;

	bool isFocus = false;	// Is this interactable currently being focused?
	// Transform player;		// Reference to the player transform

	bool hasInteracted = false;	// Have we already interacted with the object?

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

	// Called when the object starts being focused
	public void OnFocused (Transform playerTransform)
	{
		isFocus = true;
		// hasInteracted = false;
		// player = playerTransform;
    }

	// Called when the object is no longer focused
	public void OnDefocused ()
	{
		isFocus = false;
		// hasInteracted = false;
		// player = null;
	}

	// This method is meant to be overwritten
	public virtual void Interact ()
	{
		Debug.Log("Item picked up!");
	}

	void OnDrawGizmosSelected ()
	{
		if (interactionTransform == null) {
			interactionTransform = transform;
		}
		
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}


}

