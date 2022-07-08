using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFreeSwim : MonoBehaviour
{
    [SerializeField] float biteRange = 3f;
    [SerializeField] float swimSpeed = 1f;
    [SerializeField] float fishTurnSpeed = 4f;
    [HideInInspector] public bool freeSwim;
    [HideInInspector] public bool thisFish;

    Transform rig;
    Vector3 newRotation;
    BobberFloat bobberFloat;
    FreeFishHandler freeFishHandler;
    
    float swimX = 0f;
    float swimZ = 0f;
    float distanceToTarget;
    float swimDirectionX = 0f;
    float swimDirectionZ = 0f;
    float timeToChangeDir = 5f;
    

    void Start()
    {
        freeFishHandler = GetComponentInParent<FreeFishHandler>();
        
        bobberFloat = FindObjectOfType<BobberFloat>();
        rig = GameObject.FindGameObjectWithTag("Rig").transform;
        
        freeSwim = true;
        FreeSwim();
    }

    
    void Update()
    {
        if(!freeSwim) { return; }

        Swim();
        BaitCheck();
    }


    void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Shoreline") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Underwater"))
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
            timeToChangeDir = Random.Range(10, 15);
            swimDirectionX = Random.Range(-.9f, .9f);
            swimDirectionZ = Random.Range(-.9f, .9f);

            swimX = swimDirectionX * swimSpeed * Time.fixedDeltaTime;
            swimZ = swimDirectionZ * swimSpeed * Time.fixedDeltaTime;
           
            newRotation = new Vector3(swimX, 0f, swimZ);
            newRotation = newRotation.normalized * Time.fixedDeltaTime; 
    
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
        distanceToTarget = Vector3.Distance(rig.position, transform.position);

        if(distanceToTarget <= biteRange)
        {
            if(!bobberFloat.isFloating) { return; }
            if(freeFishHandler.hasFish) { return; }

            freeFishHandler.SetCurrentFish(gameObject);

            if(!thisFish) { return; }
  
            freeSwim = false;
            StopCoroutine("ChangeDirection");
        }
    }


    public void ResetFishFreeSwim()
    {
        if(freeSwim) { return; }

        freeSwim = true;        
        FreeSwim();
    }


    void OnDrawGizmos()
    {
        Gizmos.color = new Color(102, 161, 255);
        Gizmos.DrawWireSphere(transform.position, biteRange);
    }

}
