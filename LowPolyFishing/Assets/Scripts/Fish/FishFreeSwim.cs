using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFreeSwim : MonoBehaviour
{
    [SerializeField] float biteRange = 5f;

    bool isBiting;
    float swimX = 0f;
    float swimZ = 0f;
    float distanceToTarget;
    float swimDirectionX = 0f;
    float swimDirectionZ = 0f;
    float timeToChangeDir = 5f;

    Transform bait;
    Vector3 newRotation;
    FishMovement fishMovement;

    public bool freeSwim;
    public float swimSpeed = 5f;
    public float fishTurnSpeed = 1f;


    void Start()
    {

        fishMovement = GetComponent<FishMovement>();
        bait = GameObject.FindGameObjectWithTag("Rig").transform;

        FreeSwim();

        isBiting = false;   
        freeSwim = true;
        fishMovement.enabled = false;
    }

    
    void Update()
    {
        if(isBiting) { return; }
        if(!freeSwim) { return; }

        Swim();
        BaitCheck();
    }


    void OnCollisionEnter(Collision other) 
    {
        
        if(other.gameObject.CompareTag("Shoreline") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Underwater") || other.gameObject.CompareTag("Dock"))
        {
            swimX *= -1f;
            swimZ *= -1f;
            newRotation = new Vector3(swimX, 0f, swimZ);
        }    
    }


    public void FreeSwim()
    {
        StartCoroutine("ChangeDirection");
    }


    IEnumerator ChangeDirection()
    {
        while(true)
        {
            swimDirectionX = Random.Range(-1f, 2f);
            swimDirectionZ = Random.Range(-1f, 2f);
            timeToChangeDir = Random.Range(5, 10);

            swimX = swimDirectionX * swimSpeed * Time.deltaTime;
            swimZ = swimDirectionZ * swimSpeed * Time.deltaTime;
           
            newRotation = new Vector3(swimX, 0f, swimZ);
            newRotation = newRotation.normalized * Time.deltaTime; 
            
            yield return new WaitForSeconds(timeToChangeDir);
        }
    }


    void Swim()
    {
        transform.Translate(newRotation, Space.World);

        if(newRotation != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), Time.deltaTime * fishTurnSpeed);
        }
    }


    void BaitCheck()
    {
        distanceToTarget = Vector3.Distance(bait.position, transform.position);

        if(distanceToTarget <= biteRange)
        {
            fishMovement.enabled = true;
            enabled = false;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(102, 161, 255);
        Gizmos.DrawWireSphere(transform.position, biteRange);
    }

}
