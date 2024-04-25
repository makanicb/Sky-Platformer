using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletTimeController : MonoBehaviour
{
    //timeScale variables
    private float timeScale;
    public float slowScale;
    private float normScale;
    //Speed to switch between modes
    public float shiftSpeed;
    private float shiftVel;
    //the default fixedDeltaTime
    private float fixedDeltaTime;
    // Start is called before the first frame update
    void Start()
    {
        this.timeScale = Time.timeScale;
        normScale = this.timeScale;
        shiftVel = shiftSpeed;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        this.timeScale = Mathf.Clamp(this.timeScale + shiftVel * Time.unscaledDeltaTime, slowScale, normScale);
        Time.timeScale = this.timeScale;
        Time.fixedDeltaTime = this.fixedDeltaTime * this.timeScale;
    }

    void OnSlowTime()
    {
        Debug.Log("Slow Time");
        shiftVel *= -1;
    }
}
