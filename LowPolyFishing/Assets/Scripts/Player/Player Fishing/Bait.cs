using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    Reeling reeling;
    Casting casting;
    FishSchool fishSchool;

    bool fishOn;
    bool hasHingeJoint;


    void Start()
    {
        reeling = GetComponentInParent<Reeling>();
        casting = GetComponentInParent<Casting>();
        fishSchool = FindObjectOfType<FishSchool>();
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
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.EnteredFishSchool();
        }

        if(other.gameObject.CompareTag("ReelingCollider"))   //Was "Shoreline"
        {
            if(fishOn)
            {
                reeling.FishCaught();   //Was ShorelineCollision()
            }
            else
            {
                casting.ResetCast();
            }

            fishOn = false;
        }
    }


    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("FishSchool"))   
        {
            fishSchool.ExitedFishSchool();
        }    
    }


    public void ResetBait()
    {
        hasHingeJoint = false;
        Destroy(GetComponent<HingeJoint>());
    }
}
