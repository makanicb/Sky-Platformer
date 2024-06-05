using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private List<string> scenesToLoad;
    [SerializeField] private List<string> scenesToUnload;
    private bool loaded;

    private void Start()
    {
        loaded = false;
    }

    public void LoadScenes()
    {
        foreach(string scene in scenesToLoad)
        {
            if(!SceneManager.GetSceneByName(scene).isLoaded)
            {
                Debug.Log("Load Scene" + scene);
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            }
        }
        foreach(string scene in scenesToUnload)
        {
            if(SceneManager.GetSceneByName(scene).isLoaded)
            {
                Debug.Log("Load Scene" + scene);
                SceneManager.UnloadSceneAsync(scene);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !loaded)
        {
            loaded = true;
            LoadScenes();
        }
    }
}
