using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{
    public void ResetScene()
    {
        SceneManager.LoadScene("DemoScene");
    }

    void OnReset()
    {
        Debug.Log("Reset");
        ResetScene();
    }


}
