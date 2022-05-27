using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{
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

        bobber_GO.gameObject.SetActive(false);
        bait_GO.gameObject.SetActive(false);
        lineRenderer.enabled = false;

        slider.value = 0f;
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        initCastStrength = castStrength;
        fishingRod.SetActive(false);
        castStrengthCanvas.enabled = false;
        biteIndicator.SetActive(false);
    }


    void Update()
    {
        if(!isCasting) { return; }
        CastDistance();   
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Ground"))
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
            return;
        }

        if(value.isPressed)
        {
            castStrengthCanvas.enabled = true;
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
        playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        animator.SetBool(isWalkingAnim, false);
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

        bait.ResetBait();
        reeling.ResetReeling();
        playerFishing.StopFishing();
        biteIndicator.SetActive(false);
        fishMovement.ResetFish();
        fishFreeSwim.ResetFishFreeSwim();
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

}
