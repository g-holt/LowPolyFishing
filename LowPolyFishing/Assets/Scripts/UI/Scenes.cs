using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    [SerializeField] float loadDelay;
    [SerializeField] AudioClip btnPressSFX;

    AudioSource audioSource;

    int currentSceneIndex;


    void Start() 
    {
        audioSource = GetComponent<AudioSource>(); 
        audioSource.clip = btnPressSFX;   
    }


    public void LoadNextScene()
    {
        if(currentSceneIndex == 0)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
        StartCoroutine("LoadNextLevel");
    }


    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(loadDelay);

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Time.timeScale = 1;

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
    {
        audioSource.PlayOneShot(audioSource.clip);
        Application.Quit();
    }
}
