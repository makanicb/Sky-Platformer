using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    // Instaniate vars
    public GameObject usedItem;
    public GameObject launchOrigin;
    public float launchHeightFactor;
    public float launchVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        GameObject shot = Instantiate(usedItem, launchOrigin.transform.position, launchOrigin.transform.rotation);
        shot.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, launchVelocity * launchHeightFactor, launchVelocity), ForceMode.Impulse);

        // Use Coroutine to get delay
        StartCoroutine(SetFalse(shot));
    }

    // Destroy prefab 5 seconds after it has been launched
    IEnumerator SetFalse(GameObject shot)
    {
        yield return new WaitForSeconds(5.0f);
        shot.SetActive(false);
    }
}
