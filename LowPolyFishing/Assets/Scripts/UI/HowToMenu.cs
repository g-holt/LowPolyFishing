using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuOverlay;
    [SerializeField] GameObject OpenMenuBtn;
    [SerializeField] GameObject startMenu;

    Casting casting;
    PlayerMovement playerMovement;


    void Start() 
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            startMenu.SetActive(true);
        }
        
        MenuOverlay.SetActive(false);
        OpenMenuBtn.SetActive(true);
    }


    public void OpenMenu()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            startMenu.SetActive(false);
        }
        else
        {
            casting = FindObjectOfType<Casting>();
            playerMovement = FindObjectOfType<PlayerMovement>();
            casting.ResetCast();
            playerMovement.inMenu = true;
        }
        
        MenuOverlay.SetActive(true);
        OpenMenuBtn.SetActive(false);

    }


    public void CloseMenu()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            startMenu.SetActive(true);
        }
        else
        {
            playerMovement.inMenu = false;
        }

        MenuOverlay.SetActive(false);
        OpenMenuBtn.SetActive(true);
    }

}
