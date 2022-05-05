using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FishSchoolHandler : MonoBehaviour
{
    List<Transform> fishSchoolList = new List<Transform>();
    
    Casting casting;
    FishSchool fishSchool;
    FishMovement currentFish;

//FishingTimer
    [SerializeField] float timerMin = 25f;
    [SerializeField] float timerMax = 60f;
    [SerializeField] float timeToSetHook = 5f;
    [SerializeField] public GameObject biteIndicator;


    GameObject rig;
    FishMovement fishMovement;
    PlayerMovement playerMovement;
    IEnumerator fishingTimer;

    bool canSetHook;
    float timerToBite;
    float timeBeforBite;
    float timerToHookset;

    public bool fishOn;
    public bool thisFish;
    public bool isFishing;




    void Start()
    {
        casting = FindObjectOfType<Casting>();

        PopulateLists();




                isFishing = false;
        canSetHook = false;
        //fish.SetActive(false);
        biteIndicator.SetActive(false);
        //fishingTimer = FishingTimer();

        rig = GameObject.FindGameObjectWithTag("Rig");
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    void PopulateLists()
    {
        foreach(Transform child in transform)
        {
            fishSchoolList.Add(child);
        }
    }


    public void SetSchool(Transform school)
    {
        foreach(Transform child in fishSchoolList)
        {
            if(school.position == child.position)
            {Debug.Log("Set School: " + child.name);
                fishSchool = child.GetComponentInChildren<FishSchool>();
                //fishSchool.thisFish = true;
                //fishSchool.isFishing = true;  
                thisFish = true;
                isFishing = true;
                currentFish = child.Find("Fish Container").GetComponent<FishMovement>();
            }
        }
    }


    public void ResetFish()
    {
        if(fishSchool == null) { Debug.Log("fischool is null");}
        if(fishSchool != null) { Debug.Log("fischool is not null");}
        
        foreach(Transform child in fishSchoolList)
        {
            currentFish.ResetFish();
            ResetFishSchool();
            fishOn = false;
            isFishing = false;
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
    {Debug.Log("FishingTimer()");
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

        isFishing = false;
        canSetHook = false;
        biteIndicator.SetActive(false);
        StopCoroutine(fishingTimer);
    }
}
