using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector2 delta;
    private Transform _TF;
    private Transform _LF;
    public float speed;
    public Transform Camera;
    public float maxCameraDist;
    // Start is called before the first frame update
    void Start()
    {
        delta = new Vector2(0f, 0f);
        _TF = gameObject.GetComponent<Transform>();
        _LF = _TF.GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _TF.Rotate(0f, delta.x * speed * Time.deltaTime, 0f, Space.World);
        _LF.Rotate(delta.y * speed * Time.deltaTime, 0f, 0f, Space.Self);
        Debug.DrawRay(_TF.position, _LF.forward * maxCameraDist);
        
    }

    private void FixedUpdate()
    {
        Vector3 cameraDir = -_LF.forward;
        RaycastHit rayHit;
        Vector3 cameraPosition;
        if(Physics.Raycast(_TF.position, cameraDir, out rayHit, maxCameraDist))
        {
            cameraPosition = rayHit.point;
        }
        else
        {
            cameraPosition = _LF.position + cameraDir * maxCameraDist;
        }
        Camera.position = cameraPosition;
        Camera.LookAt(_LF);
    }

    void OnLook(InputValue lookValue)
    {
        delta = lookValue.Get<Vector2>();
    }
}
