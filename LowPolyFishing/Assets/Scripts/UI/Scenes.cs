using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    [SerializeField] float loadDelay;

    int currentSceneIndex;


    // void Awake()
    // {
    //     int numScenePersists = FindObjectsOfType<Scenes>().Length;

    //     //If this gameObject already Exists Destroy
    //     if(numScenePersists > 1)
    //     {
    //         Destroy(gameObject);
    //     }
    //     else
    //     {
    //         DontDestroyOnLoad(gameObject);
    //     }
    // }


    // public void ResetUIPersist()
    // {
    //     Destroy(gameObject);
    // }


    public void LoadNextScene()
    {
        Debug.Log("Start Game");
        StartCoroutine("LoadNextLevel");
    }


    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;
Debug.Log("Current Scene Index: " + currentSceneIndex.ToString());
        if(currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }


    public void QuitGame()
    {Debug.Log("Quit Game");
        Application.Quit();
    }
}
