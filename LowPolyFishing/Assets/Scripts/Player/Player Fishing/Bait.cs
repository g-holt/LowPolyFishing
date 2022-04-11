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

            GetComponent<HingeJoint>().connectedBody = GameObject.FindGameObjectWithTag("FishContainer").GetComponent<Rigidbody>();
            GetComponent<HingeJoint>().enableCollision = true;
            hasHingeJoint = true;
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  
        if(other.gameObject.CompareTag("FishSchool"))
        {
            fishSchool.EnteredFishSchool();
        }

        if(other.gameObject.CompareTag("Shoreline"))
        {
            reeling.ShoreLineCollision();
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
