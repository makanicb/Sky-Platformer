using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] float pullSpeed = 0.5f;
    [SerializeField] float stopDistance = 4f;
    [SerializeField] GameObject hookPrefab;
    [SerializeField] Transform shootTransform;

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
            wishHook = false;
        }
        else if(hook != null && wishRelease)
        {
            DestroyHook();
            wishRelease = false;
        }

        if (!pulling || hook == null) return;

        if(Vector3.Distance(transform.position, hook.transform.position) <= stopDistance)
        {
            DestroyHook();
        }
        else
        {
            rigid.AddForce((hook.transform.position - transform.position).normalized * pullSpeed, ForceMode.VelocityChange);
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
