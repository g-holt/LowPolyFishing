using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishSchoolHandler : MonoBehaviour
{
    [SerializeField] float timerMin = 25f;
    [SerializeField] float timerMax = 60f;
    [SerializeField] float timeToSetHook = 5f;
    [SerializeField] public GameObject biteIndicator;

    FishMovement[] allFish;
    List<Transform> fishSchoolList = new List<Transform>();
    
    GameObject rig;
    Casting casting;
    FishMovement currentFish;
    IEnumerator fishingTimer;
    PlayerMovement playerMovement;

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
        biteIndicator.SetActive(false);

        casting = FindObjectOfType<Casting>();
        allFish = FindObjectsOfType<FishMovement>();
        rig = GameObject.FindGameObjectWithTag("Rig");
        playerMovement = FindObjectOfType<PlayerMovement>();

        PopulateLists();
        SetFishInactive();
    }


    void PopulateLists()
    {
        foreach(Transform child in transform)
        {
            fishSchoolList.Add(child);
        }
    }


    void SetFishInactive()
    {
        for(int i = 0; i < allFish.Length; i++)
        {
            allFish[i].gameObject.SetActive(false);
        }
    }


    public void SetSchool(Transform school)
    {
        foreach(Transform child in fishSchoolList)
        {
            if(school.position == child.position)
            {
                thisFish = true;
                isFishing = true;
                currentFish = child.Find("Fish Container").GetComponent<FishMovement>();
            }
        }
    }


    public void EnteredFishSchool()
    {
        if(fishOn) { return; }
        
        isFishing = true;

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
    }


    IEnumerator FishingTimer()
    {Debug.Log("Fishing Timer");
        timeBeforBite = Random.Range(timerMin, timerMax);

        while(!canSetHook)
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

            canSetHook = true;
            timerToBite = 0f;
        }

        timerToBite += Time.deltaTime;
    }


    void HooksetTimer()
    {
        if(Mouse.current.rightButton.isPressed)
        {
            if(!isFishing) { return; }

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

        currentFish.gameObject.SetActive(true);
        playerMovement.fishOn = true;
    
        currentFish.GetComponent<FishMovement>().onHook = true;
    }


    void ResetFishingTimer()
    { Debug.Log("ResetTimer");
        biteIndicator.SetActive(false);

        isFishing = true;
        canSetHook = false;
        timerToHookset = 0f;

        StopCoroutine("FishingTimer");
        StartCoroutine("FishingTimer");
    }


    public void ResetFishSchoolHandler()
    { Debug.Log("ResetSchool");
        //fishingTimer = FishingTimer();
        currentFish.ResetFish();
        isFishing = false;
        canSetHook = false;
        fishOn = false;
        biteIndicator.SetActive(false);
        StopCoroutine("FishingTimer");
    }


    // public void ResetFish()
    // {
    //     foreach(Transform child in fishSchoolList)
    //     {
            
    //         //ResetFishSchool();
    //         fishOn = false;
    //         isFishing = false;
    //     }
    // }
}
