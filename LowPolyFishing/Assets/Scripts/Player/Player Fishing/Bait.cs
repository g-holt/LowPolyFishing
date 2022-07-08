using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{
    [SerializeField] GameObject biteIndicator;

    Reeling reeling;
    Casting casting;
    PlayerMovement playerMovement;

    bool fishOn;
    bool hasHingeJoint;


    void Start()
    {   
        hasHingeJoint = false;

        reeling = GetComponentInParent<Reeling>();
        casting = GetComponentInParent<Casting>();
        playerMovement = GetComponentInParent<PlayerMovement>();
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("FishContainer"))
        {
            if(hasHingeJoint) { return; }

            HingeJoint hingeJoint = gameObject.AddComponent<HingeJoint>() as HingeJoint;

            hingeJoint.connectedBody = other.gameObject.GetComponent<Rigidbody>();
            hingeJoint.enableCollision = true;
            hingeJoint.axis = new Vector3(0f, 1f, 0f);
            hingeJoint.anchor = new Vector3(0f, 0f, 0f);

            fishOn = true;
            hasHingeJoint = true;
            playerMovement.fishOn = true;

            StartCoroutine("DisplayBiteIndicator");
        }
    }


    private void OnTriggerEnter(Collider other) 
    {  
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


    IEnumerator DisplayBiteIndicator()
    {
        biteIndicator.SetActive(true);
        yield return new WaitForSeconds(3);
        biteIndicator.SetActive(false);
    }


    public void ResetBait()
    {
        if(!fishOn) { return; }

        fishOn = false;
        hasHingeJoint = false;
        Destroy(GetComponent<HingeJoint>());
    }

}
