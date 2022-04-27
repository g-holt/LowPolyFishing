using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Casting : MonoBehaviour
{
    [Header("Cast Strength")]
    [SerializeField] float verticalCastStrength = 5f;
    //[SerializeField] public float horizontalCastStrength = 10f;
    [SerializeField] float castStrength = .1f;
    [SerializeField] Canvas castStrengthCanvas;
    [Header("Gear")]
    [SerializeField] GameObject gear_GO;
    [SerializeField] GameObject bait_GO;
    [SerializeField] GameObject bobber_GO;
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

    bool isCasting;
    string isFishingAnim;
    string isWalkingAnim;
    float initCastStrength;
    
    public bool canFish;


    void OnEnable() 
    {
        rb = GetComponent<Rigidbody>();    
        reeling = GetComponent<Reeling>();
        bait = GetComponentInChildren<Bait>();
        bobberFloat = GetComponent<BobberFloat>();
        animator = GetComponentInParent<Animator>();
        lineRenderer = GetComponent<LineRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        slider = castStrengthCanvas.GetComponentInChildren<Slider>();

        playerFishing = FindObjectOfType<PlayerFishing>();
        fishSchool = FindObjectOfType<FishSchool>();
        fishMovement = FindObjectOfType<FishMovement>();

        bobber_GO.gameObject.SetActive(false);
        bait_GO.gameObject.SetActive(false);
        lineRenderer.enabled = false;

        slider.value = 0f;
        isFishingAnim = "IsFishing";
        isWalkingAnim = "IsWalking";
        initCastStrength = castStrength;
    }


    void Update()
    {
        if(Keyboard.current.rKey.isPressed)
        {
            ResetCast();
        }

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
            slider.value = 0f;
            isCasting = false;
            castStrengthCanvas.enabled = false;
            CastLine();
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
        //casting.horizontalCastStrength = castStrength;
        playerMovement.isFishing = true;

        fishingRod.SetActive(true);

        animator.SetBool(isWalkingAnim, false);
        animator.SetBool(isFishingAnim, true);

        playerInput.SwitchCurrentActionMap("Fishing");
        castStrength = initCastStrength;
    }


    public void ThrowLine()
    {
        rb.useGravity = true;
        lineRenderer.enabled = true;
        bait_GO.gameObject.SetActive(true);
        bobber_GO.gameObject.SetActive(true);

        float throwX = transform.forward.x * castStrength * 100;  //float throwX = transform.forward.x * horizontalCastStrength * 100;
        float throwY = verticalCastStrength * 100;
        float throwZ = transform.forward.z * castStrength * 100;  //float throwZ = transform.forward.z * horizontalCastStrength * 100;

        Vector3 castForce = new Vector3(throwX, throwY, throwZ);
        rb.AddForce(castForce);
    }


    //Casting Animation Event
    public void HandleBobber()
    {
        ThrowLine();
    }


    public void ResetCast()
    {
        if(fishSchool.fishOn)
        {
            bait.ResetBait();
            fishMovement.ResetFish();
        }

        playerFishing.StopFishing();
        HandleReset();
        reeling.ResetReeling();
    }


    void HandleReset()
    {
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
