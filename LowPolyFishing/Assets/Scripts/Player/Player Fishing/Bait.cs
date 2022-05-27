using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    Reeling reeling;
    Casting casting;
    FishSchool fishSchool;
    FishSchoolHandler fishSchoolHandler;
    PlayerMovement playerMovement;

    bool fishOn;
    bool hasHingeJoint;


    void Start()
    {   
        hasHingeJoint = false;

        reeling = GetComponentInParent<Reeling>();
        casting = GetComponentInParent<Casting>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        fishSchoolHandler = FindObjectOfType<FishSchoolHandler>();
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("FishContainer"))
        {
            if(hasHingeJoint) { return; }

            HingeJoint hingeJoint = gameObject.AddComponent<HingeJoint>() as HingeJoint;

            hingeJoint.connectedBody = GameObject.FindGameObjectWithTag("FishContainer").GetComponent<Rigidbody>();
            hingeJoint.enableCollision = true;
            hingeJoint.axis = new Vector3(0f, 1f, 0f);
            hingeJoint.anchor = new Vector3(0f, 0f, 0f);

            fishOn = true;
            hasHingeJoint = true;
            playerMovement.fishOn = true;
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  
        if(other.gameObject.CompareTag("FishSchool"))
        {
            //fishSchool = other.gameObject.GetComponent<FishSchool>();
            fishSchoolHandler.SetSchool(other.gameObject.transform);
            //fishSchool.EnteredFishSchool();
            fishSchoolHandler.EnteredFishSchool();
        }

        if(other.gameObject.CompareTag("ReelingCollider"))
        {
            if(fishOn)
            {
                reeling.FishCaught();
            }
            else
            {
                casting.ResetCast();
            }
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("FishSchool"))   
        {
            //fishSchool.ExitedFishSchool();
            fishSchoolHandler.ExitedFishSchool();
        }    
    }


    public void ResetBait()
    {
        if(!fishOn) { return; }

        fishOn = false;
        hasHingeJoint = false;
        Destroy(GetComponent<HingeJoint>());
    }

}
