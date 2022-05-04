using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishSchool : MonoBehaviour
{
    [SerializeField] float timerMin = 25f;
    [SerializeField] float timerMax = 60f;
    [SerializeField] float timeToSetHook = 5f;
    [SerializeField] public GameObject biteIndicator;
    [SerializeField] GameObject fish;

    GameObject rig;
    FishMovement fishMovement;
    PlayerMovement playerMovement;
    IEnumerator fishingTimer;

    //bool isFishing;
    bool canSetHook;
    float timerToBite;
    float timeBeforBite;
    float timerToHookset;

    public bool fishOn;
    public bool thisFish;
    public bool isFishing;

    void Start()
    {
        isFishing = false;
        canSetHook = false;
        fish.SetActive(false);
        biteIndicator.SetActive(false);
        //fishingTimer = FishingTimer();

        rig = GameObject.FindGameObjectWithTag("Rig");
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    public void EnteredFishSchool()
    {
        if(fishOn) { return; }
        
        isFishing = true;
        //StartCoroutine("FishingTimer");
        fishingTimer = FishingTimer();
        StartCoroutine(fishingTimer);
    }


    public void ExitedFishSchool()
    {
        if(fishOn) { return; }

        isFishing = false;
        canSetHook = false;

        timerToBite = 0f;
        timerToHookset = 0f;

        biteIndicator.SetActive(false);
        //StopCoroutine("FishingTimer");
        //StopCoroutine(fishingTimer);
    }
void Update() {
    Debug.Log(isFishing);
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
        if(timerToBite >= timeBeforBite)
        {
            biteIndicator.SetActive(true);

            isFishing = false;
            canSetHook = true;
            timerToBite = 0f;
        }

        timerToBite += Time.deltaTime;
    }


    void HooksetTimer()
    {Debug.Log("hookset isFishing: " + isFishing + " " +  gameObject.name);
        if(Mouse.current.rightButton.isPressed)
        {Debug.Log(isFishing);
            if(!isFishing) { return; }
            Debug.Log("Catch Fish");
            biteIndicator.SetActive(false);

            canSetHook = false;
            timerToHookset = 0f;
            CatchFish();
        }

        if(timerToHookset >= timeToSetHook)
        {
            ResetFishingTimer();
        }

        timerToHookset += Time.deltaTime;
    }


    void CatchFish()
    {
        if(!thisFish) { return; }

        fishOn = true;
        fish.SetActive(true);
        playerMovement.fishOn = true;
        fish.GetComponent<FishMovement>().onHook = true;
    }


    void ResetFishingTimer()
    {
        biteIndicator.SetActive(false);

        isFishing = true;
        canSetHook = false;
        timerToHookset = 0f;

        StopCoroutine(fishingTimer);
        StartCoroutine(fishingTimer);
    }


    public void ResetFishSchool()
    {
        fishingTimer = FishingTimer();
        Debug.Log("reset fishschool: before: " + isFishing);
        isFishing = false;
        canSetHook = false;
        biteIndicator.SetActive(false);
        StopCoroutine(fishingTimer);
        Debug.Log("reset fishschool: after: " + isFishing);
    }
}
