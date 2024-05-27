using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTogether : MonoBehaviour
{
    [SerializeField] Transform[] followers;

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            foreach(Transform t in followers)
            {
                t.LookAt(hit.point);
            }
        }
        else
        {
            foreach(Transform t in followers)
            {
                t.rotation = transform.rotation;
            }
        }
        
    }
}
