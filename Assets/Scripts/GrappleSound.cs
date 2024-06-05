using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSound : MonoBehaviour
{
    public GameObject Grapple;

    // Start is called before the first frame update
    void Start()
    {
        Grapple.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grapple();
        }

        if (Input.GetMouseButtonUp(0))
        {
            release();
        }
    }

    void grapple()
    {
        Grapple.SetActive(true);
    }

    void release()
    {
        Grapple.SetActive(false);
    }
}
