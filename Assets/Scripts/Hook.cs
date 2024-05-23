using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    [SerializeField] float HookForce;

    Grapple grapple;
    Rigidbody rigid;
    LineRenderer lineRenderer;

    public void Initialize(Grapple grapple, Transform shootTransform) {
        transform.forward = shootTransform.forward;
        this.grapple = grapple;
        rigid = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Grapple"), QueryTriggerInteraction.Collide))
        {
            Debug.Log("I hit something!");
            transform.position = hit.point;
            attach();
        }
        else
        {
            Debug.Log("I did not hit something");
            rigid.AddForce(transform.forward * HookForce, ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (grapple != null)
        {
            Vector3[] positions = new Vector3[]
                {
                transform.position,
                grapple.transform.position
                };

            lineRenderer.SetPositions(positions);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if((LayerMask.GetMask("Grapple") & 1 << other.gameObject.layer) > 0) {
            attach();
        }
    }

    private void attach()
    {
        rigid.useGravity = false;
        rigid.isKinematic = true;

        grapple.StartPull();
    }
}
