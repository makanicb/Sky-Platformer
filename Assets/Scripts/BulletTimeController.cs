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

    //Ability limiter
    public float duration;
    public float chargeRate;
    private float rDuration;
    private float rate;

    //ability toggle
    private bool slow;

    // Start is called before the first frame update
    void Start()
    {
        this.timeScale = Time.timeScale;
        normScale = this.timeScale;
        shiftVel = shiftSpeed;
        this.fixedDeltaTime = Time.fixedDeltaTime;
        rDuration = duration;
        rate = chargeRate;
        slow = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.timeScale = Mathf.Clamp(this.timeScale + shiftVel * Time.unscaledDeltaTime, slowScale, normScale);
        Time.timeScale = this.timeScale;
        Time.fixedDeltaTime = this.fixedDeltaTime * this.timeScale;
        rDuration = Mathf.Clamp(rDuration + rate * Time.deltaTime, 0f, duration);
        //Debug.Log(rDuration);
        if(rDuration == 0)
        {
            slow = false;
            rate = chargeRate;
            shiftVel = shiftSpeed;
        }
    }

    void OnSlowTime()
    {
        shiftVel *= -1;
        slow = !slow;
        if(slow)
        {
            rate = -1f;
        }
        else
        {
            rate = chargeRate;
        }
    }

    public float getRemainingDuration()
    {
        return rDuration;
    }
}
