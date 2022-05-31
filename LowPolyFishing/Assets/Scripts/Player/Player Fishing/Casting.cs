using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Casting : MonoBehaviour
{
    [SerializeField] Canvas fishingAreaCanvas;
    [SerializeField] TextMeshProUGUI fishingAreaText;
    [SerializeField] float textFadeSpeed = 1f;
    [Header("Cast Strength")]
    [SerializeField] float verticalCastStrength = 5f;
    [SerializeField] float castStrength = .5f;
    [SerializeField] Canvas castStrengthCanvas;
    [Header("Gear")]
    [SerializeField] GameObject gear_GO;
    [SerializeField] GameObject bait_GO;
    [SerializeField] GameObject bobber_GO;
    [SerializeField] GameObject biteIndicator;
    public GameObject fishingRod;


    Bait bait;
    Rigidbody rb;
    Slider slider;
    Reeling reeling;
    Animator animator;
    FishSchool fishSchool;
    BobberFloat bobberFloat;
    PlayerInput playerInput;
    LineRenderer lineRenderer;
    FishMovement fishMovement;
    PlayerFishing playerFishing;
    PlayerMovement playerMovement;
    FishSchoolHandler fishSchoolHandler;
    FishFreeSwim fishFreeSwim;
    FreeFishHandler freeFishHandler;

    bool canThrow;
    bool isCasting;
    string isFishingAnim;
    string isWalkingAnim;
    float initCastStrength;
    float horizontalCastStrength;
    
    public bool canFish;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        reeling = GetComponent<Reeling>();
        bait = GetComponentInChildren<Bait>();
        bobberFloat = GetComponent<BobberFloat>();
        animator = GetComponentInParent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        playerInput = GetComponentInParent<PlayerInput>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        slider = castStrengthCanvas.GetComponentInChildren<Slider>();

        playerFishing = FindObjectOfType<PlayerFishing>();
        fishSchool = FindObjectOfType<FishSchool>();
        fishSchoolHandler = FindObjectOfType<FishSchoolHandler>();
        fishMovement = FindObjectOfType<FishMovement>();
        fishFreeSwim = FindObjectOfType<FishFreeSwim>();
        freeFishHandler = FindObjectOfType<FreeFishHandler>();

        bobber_GO.gameObject.SetActive(false);
        bait_GO.gameObject.SetActive(false);
        lineRenderer.enabled = false;

        slider.value = 0f;
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        initCastStrength = castStrength;
        fishingAreaCanvas.enabled = false;
        castStrengthCanvas.enabled = false;
        fishingRod.SetActive(false);
        biteIndicator.SetActive(false);
    }


    void Update()
    {
        if(!isCasting) { return; }
        CastDistance();   
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(playerMovement.fishOn) { return; }
        
        if(!other.gameObject.CompareTag("WaterSurface") && !other.gameObject.CompareTag("FishContainer") && !other.gameObject.CompareTag("Shoreline"))
        {
            ResetCast();
        }
    }


    void OnCast(InputValue value)
    {
        canThrow = true;

        if(!canFish)
        {
            Debug.Log("You are not in a fish Zone");
            fishingAreaCanvas.enabled = true;
            StartCoroutine("FadeText");
            return;
        }

        if(value.isPressed)
        {
            if(fishingAreaCanvas.enabled) { fishingAreaCanvas.enabled = false; }

            animator.SetBool(isWalkingAnim, false);

            castStrengthCanvas.enabled = true;
            playerMovement.isCasting = true;
            isCasting = true;
        }
        else if(!value.isPressed)
        {
            CastLine();
            slider.value = 0f;
            isCasting = false;
            castStrengthCanvas.enabled = false;
        }
    }


    void CastDistance()
    {
        castStrength += castStrength * Time.deltaTime;
        
        if(castStrength >= 5)
        {
            castStrength = 5f;
        }

        slider.value = castStrength;
    }


    void CastLine()
    {
        horizontalCastStrength = castStrength;
        //playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        //animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
        castStrength = initCastStrength;
    }


    public void ThrowLine()
    {
        if(!canThrow) { return; }
    
        rb.useGravity = true;
        lineRenderer.enabled = true;
        bait_GO.gameObject.SetActive(true);
        bobber_GO.gameObject.SetActive(true);

        float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 castForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(castForce);
    }


    public void ResetCast()
    {
        playerMovement.fishOn = false;
        playerMovement.isCasting = false;

        bait.ResetBait();
        reeling.ResetReeling();
        playerFishing.StopFishing();
        biteIndicator.SetActive(false);
        freeFishHandler.ResetFish();
        //fishMovement.ResetFish();
        //fishFreeSwim.ResetFishFreeSwim();
        //fishSchoolHandler.ResetFishSchoolHandler();
        
        HandleReset();
    }


    void HandleReset()
    {
        canThrow = false;
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        lineRenderer.enabled = false;
        bobberFloat.isFloating = false;
        transform.position = gear_GO.transform.position;

        bait_GO.SetActive(false);
        bobber_GO.SetActive(false);
        fishingRod.SetActive(false);
    }


    IEnumerator FadeText()
    {
        fishingAreaText.color = new Color(fishingAreaText.color.r, fishingAreaText.color.g, fishingAreaText.color.b, 1);

        while(fishingAreaText.color.a > Mathf.Epsilon)
        {
            fishingAreaText.color = new Color(fishingAreaText.color.r, fishingAreaText.color.g, fishingAreaText.color.b, fishingAreaText.color.a - (Time.deltaTime * textFadeSpeed));
            yield return null;
        }
    }

}
