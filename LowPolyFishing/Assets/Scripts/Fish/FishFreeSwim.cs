using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFreeSwim : MonoBehaviour
{
    float swimX = 0f;
    float swimZ = 0f;
    float swimDirectionX = 0f;
    float swimDirectionZ = 0f;
    float timeToChangeDir = 5f;
    float rotY;

    Vector3 newRotation;

    public float swimSpeed = 5f;
    public float fishTurnSpeed = 1f;


    void Start()
    {
        FreeSwim();
    }

    
    void Update()
    {
        Swim();
    }


    void OnCollisionEnter(Collision other) 
    {
        
        if(other.gameObject.CompareTag("Shoreline") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Underwater"))
        {Debug.Log("Collision");
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
            Debug.Log(swimDirectionX + " " + swimDirectionZ);   
            
            rotY = ((swimDirectionX + swimDirectionZ) / 2) * 360;
            //newRotation = new Vector3(0f, rotY * Time.deltaTime, 0f);
            newRotation = new Vector3(swimX, 0f, swimZ);

            yield return new WaitForSeconds(timeToChangeDir);
        }
    }


    void Swim()
    {
        RotateFish();

        transform.Translate(swimX, 0f, swimZ, Space.World);

        if(newRotation != Vector3.zero)
        {
            transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), Time.deltaTime * fishTurnSpeed);
            //transform.forward = newRotation;
        }

        //transform.Translate(swimX, 0f, swimZ, Space.World);
    }


    void RotateFish()
    {
        

        Debug.Log(newRotation.ToString());
        //transform.LookAt(newRotation, transform.up);
        //transform.Rotate(transform.up - newRotation, Space.Self);
        //transform.localRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newRotation), Time.deltaTime * 1f);
    }
}
