using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishSchool : MonoBehaviour
{
    [SerializeField] float timerMin = 25f;
    [SerializeField] float timerMax = 60f;
    [SerializeField] float timeToSetHook = 5f;
    [SerializeField] GameObject fish;

    GameObject bait;

    bool fishOn;
    bool isFishing;
    bool canSetHook;
    float timerToBite;
    float timeBeforBite;
    float timerToHookset;


    void Start()
    {
        isFishing = false;
        canSetHook = false;
        fish.SetActive(false);

        bait = GameObject.FindGameObjectWithTag("Bait");
    }


    public void EnteredFishSchool()
    {
        isFishing = true;
        StartCoroutine("FishingTimer");
    }


    public void ExitedFishSchool()
    {
        if(fishOn) { return; }

        isFishing = false;
        canSetHook = false;

        timerToBite = 0f;
        timerToHookset = 0f;

        StopCoroutine("FishingTimer");
    }


    IEnumerator FishingTimer()
    {
        timeBeforBite = Random.Range(timerMin, timerMax);

        while(isFishing)
        {
            BiteTimer();
            yield return null;  //returns to beginning of while loop
        }

        while(canSetHook)
        {
            HooksetTimer();
            yield return null;  //returns to beginning of while loop
        }
    }


    void BiteTimer()
    {
        Debug.Log(timeBeforBite);
        if(timerToBite >= timeBeforBite)
        {
            Debug.Log("Set The Hook");
            isFishing = false;
            canSetHook = true;
            timerToBite = 0f;
        }

        timerToBite += Time.deltaTime;
    }


    void HooksetTimer()
    {

        if(Mouse.current.rightButton.isPressed)
        {
            Debug.Log("Fish On!");
            canSetHook = false;
            timerToHookset = 0f;
            CatchFish();
        }

        if(timerToHookset >= timeToSetHook)
        {
            Debug.Log("Missed Hookset - Fish Escaped");
            canSetHook = false;
            timerToHookset = 0f;
        }

        timerToHookset += Time.deltaTime;
    }


    void CatchFish()
    {
        fishOn = true;
        fish.SetActive(true);
        Debug.Log("Catching Fish");
    }
}
