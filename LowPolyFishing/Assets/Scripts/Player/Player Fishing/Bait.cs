using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    Reeling reeling;
    FishSchool fishSchool;

    bool hasHingeJoint;


    void Start()
    {
        reeling = GetComponentInParent<Reeling>();
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

            hasHingeJoint = true;
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.EnteredFishSchool();
        }

        if(other.gameObject.CompareTag("FishCaught"))   //Was "Shoreline"
        {
            reeling.FishCaught();   //Was ShorelineCollision()
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
