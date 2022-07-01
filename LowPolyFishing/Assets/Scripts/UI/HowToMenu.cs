using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMenu : MonoBehaviour
{
    [SerializeField] GameObject MenuOverlay;
    [SerializeField] GameObject OpenMenuBtn;
    [SerializeField] GameObject CloseMenuBtn;


    void Start() 
    {
        MenuOverlay.SetActive(false);
        OpenMenuBtn.SetActive(true);
        CloseMenuBtn.SetActive(false);
    }



    public void OpenMenu()
    {
        MenuOverlay.SetActive(true);
        OpenMenuBtn.SetActive(false);
        CloseMenuBtn.SetActive(true);
    }


    public void CloseMenu()
    {
        MenuOverlay.SetActive(false);
        OpenMenuBtn.SetActive(true);
        CloseMenuBtn.SetActive(false);
    }

}
