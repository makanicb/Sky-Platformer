using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Vector2 delta;
    private Transform _TF;
    private Transform _LF;
    //Camera movement and placement
    public float speed;
    [SerializeField] private float webGLCo;
    public Transform Camera;
    public float maxCameraDist;
    public float maxAngle, minAngle;
    private float currentAngle;
    //fine tuning
    public float speedCo;
    public float maxCo, minCo;
    public float tuneStep;
    // Start is called before the first frame update
    void Start()
    {
        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            speed *= webGLCo;
        }
        delta = new Vector2(0f, 0f);
        _TF = gameObject.GetComponent<Transform>();
        _LF = _TF.GetChild(0).GetComponent<Transform>();
        currentAngle = _LF.localEulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        _TF.Rotate(0f, delta.x * speed * Time.deltaTime * speedCo, 0f, Space.World);
        float deltaAngle = delta.y * speed * Time.deltaTime * speedCo;
        float trueNewAngle = Mathf.Clamp(currentAngle + deltaAngle, minAngle, maxAngle);
        float trueDeltaAngle = trueNewAngle - currentAngle;
        _LF.Rotate(trueDeltaAngle, 0f, 0f, Space.Self);
        currentAngle += trueDeltaAngle;
        //Debug.Log(currentAngle);
        Debug.DrawRay(_TF.position, _LF.forward * maxCameraDist);
    }

    private void FixedUpdate()
    {
        Vector3 cameraDir = -_LF.forward;
        RaycastHit rayHit;
        Vector3 cameraPosition;
        if(Physics.Raycast(_LF.position, cameraDir, out rayHit, maxCameraDist))
        {
            cameraPosition = rayHit.point;
        }
        else
        {
            cameraPosition = _LF.position + cameraDir * maxCameraDist;
        }

        Camera.SetPositionAndRotation(cameraPosition, _LF.rotation);
        //Camera.LookAt(_LF);
    }

    void OnLook(InputValue lookValue)
    {
        delta = lookValue.Get<Vector2>();
    }

    void OnIncreaseSensitivity()
    {
        speedCo = Mathf.Clamp(speedCo + tuneStep, minCo, maxCo);
        Debug.Log("Speed Coefficient: " + speedCo);
    }

    void OnDecreaseSensitivity()
    {
        speedCo = Mathf.Clamp(speedCo - tuneStep, minCo, maxCo);
        Debug.Log("Speed Coefficient: " + speedCo);
    }
}
