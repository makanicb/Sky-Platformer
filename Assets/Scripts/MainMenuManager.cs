using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string firstScene;
    [SerializeField] private string persistentScene;
    //[SerializeField] private GameObject[] objectsToHide;
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        //HideObjects();
        SceneManager.LoadScene(persistentScene);
        SceneManager.LoadScene(firstScene, LoadSceneMode.Additive);
        //scenesToLoad.Add(SceneManager.LoadSceneAsync(firstScene));
        //StartCoroutine(LoadSceneAsync());
    }

    /*private void HideObjects()
    {
        foreach(GameObject obj in objectsToHide)
        {
            obj.SetActive(false);
        }
    }*/

    private IEnumerator LoadSceneAsync()
    {
        float loadProgress = 0f;
        for(int i = 0; i < scenesToLoad.Count; i++)
        {
            while(!scenesToLoad[i].isDone)
            {
                loadProgress += scenesToLoad[i].progress / scenesToLoad.Count;
                Debug.Log(loadProgress);
                yield return null;
            }
        }
    }
}
