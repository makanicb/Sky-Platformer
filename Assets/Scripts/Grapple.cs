using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] float pullSpeed;
    [SerializeField] float stopDistance;
    [SerializeField] public float maxDistance;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;
    //[SerializeField] Transform lookPoint;

    Hook hook;
    bool pulling;
    Rigidbody rigid;
    private bool wishHook, wishRelease, toggle;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        pulling = false;
        wishHook = false;
        wishRelease = false;
        toggle = false;

    }

    // Update is called once per frame
    void Update()
    {


        if(hook == null && wishHook)
        {
            StopAllCoroutines();
            pulling = false;
            hook = Instantiate(hookPrefab, shootTransform.position, Quaternion.identity).GetComponent<Hook>();
            hook.Initialize(this, shootTransform);
            StartCoroutine(DestroyHookAfterLifetime());
        }
        else if(hook != null && wishRelease)
        {
            DestroyHook();
        }
        wishHook = false;
        wishRelease = false;
    }

    private void FixedUpdate()
    {
        if (hook == null) return;
        float dist = Vector3.Distance(transform.position, hook.transform.position);
        Debug.Log("Distance " + dist);

        if(pulling && dist >= stopDistance)
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.Acceleration);
        }
        else if (pulling || !pulling && dist >= maxDistance)
        {
            DestroyHook();
        }
    }

    public void StartPull()
    {
        pulling = true;
    }

    private void DestroyHook()
    {
        if (hook == null) return;

        pulling = false;
        Destroy(hook.gameObject);
        hook = null;
    }

    private IEnumerator DestroyHookAfterLifetime()
    {
        yield return new WaitForSeconds(8f);

        DestroyHook();
    }

    void OnFire()
    {
        Debug.Log("PEW");
        toggle = !toggle;
        if(toggle)
        {
            wishHook = true;
        }
        else
        {
            wishRelease = true;
        }
    }
    
}
