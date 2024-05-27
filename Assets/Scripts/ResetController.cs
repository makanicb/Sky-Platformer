using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{
    private float timeScale;
    private float fixedDeltaTime;
    private bool wishReset;

    private void Start()
    {
        this.timeScale = Time.timeScale;
        this.fixedDeltaTime = Time.fixedDeltaTime;
        Debug.Log("Time Scale = " + this.timeScale + " Fixed Delta Time = " + this.fixedDeltaTime);
        wishReset = false;
    }

    private void LateUpdate()
    {
        if (wishReset)
        {
            Debug.Log("Time Scale = " + this.timeScale + " Fixed Delta Time = " + this.fixedDeltaTime);
            Time.timeScale = this.timeScale;
            Time.fixedDeltaTime = this.fixedDeltaTime;
            Debug.Log("Time Scale = " + Time.timeScale + " Fixed Delta Time = " + Time.fixedDeltaTime);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ResetScene()
    {
        wishReset = true;
    }

    void OnReset()
    {
        Debug.Log("Reset");
        ResetScene();
    }


}
