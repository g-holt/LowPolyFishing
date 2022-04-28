using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishSchool : MonoBehaviour
{
    [SerializeField] float timerMin = 25f;
    [SerializeField] float timerMax = 60f;
    [SerializeField] float timeToSetHook = 5f;
    [SerializeField] GameObject biteIndicator;
    [SerializeField] GameObject fish;

    GameObject rig;
    FishMovement fishMovement;

    bool isFishing;
    bool canSetHook;
    float timerToBite;
    float timeBeforBite;
    float timerToHookset;

    public bool fishOn;
//public bool thisFish;

    void Start()
    {
        isFishing = false;
        canSetHook = false;
        fish.SetActive(false);
        biteIndicator.SetActive(false);

        rig = GameObject.FindGameObjectWithTag("Rig");
        //fishMovement = GameObject.FindObjectOfType<FishMovement>();
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

        biteIndicator.SetActive(false);
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
            //Debug.Log("Set The Hook");
            biteIndicator.SetActive(true);

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
            biteIndicator.SetActive(false);

            canSetHook = false;
            timerToHookset = 0f;
            CatchFish();
        }

        if(timerToHookset >= timeToSetHook)
        {
            Debug.Log("Missed Hookset - Fish Escaped");
            ResetFishingTimer();
        }

        timerToHookset += Time.deltaTime;
    }


    void CatchFish()
    {
        //if(!thisFish) { return; }

        fishOn = true;
        fish.SetActive(true);
        fish.GetComponent<FishMovement>().onHook = true;
    }


    void ResetFishingTimer()
    {
        Debug.Log("ResetFishingTimer()");
        biteIndicator.SetActive(false);

        isFishing = true;
        canSetHook = false;
        timerToHookset = 0f;

        StopCoroutine("FishingTimer");
        StartCoroutine("FishingTimer");
    }
}
