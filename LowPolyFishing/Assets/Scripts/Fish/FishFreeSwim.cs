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

    public float swimSpeed = 5f;


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
        
        if(other.gameObject.CompareTag("Shoreline") || other.gameObject.CompareTag("Ground"))
        {Debug.Log("Collision");
            // if(swimDirectionX == 0)
            // {
            //     swimDirectionX = 1;
            // }
            // else
            // {
            //     swimDirectionX = 0;
            // }

            // if(swimDirectionZ == 0)
            // {
            //     swimDirectionZ = 1;
            // }
            // else
            // {
            //     swimDirectionZ = 0;
            // }
            swimX *= -1f;
            swimZ *= -1f;
        }    
    }


    public void FreeSwim()
    {
        StartCoroutine("ChangeDirection");
    }


    IEnumerator ChangeDirection()
    {
        while(true)
        {//Debug.Log("ChangeDirection: " + timeToChangeDir);
            swimDirectionX = Random.Range(-1, 2);
            swimDirectionZ = Random.Range(-1, 2);
            timeToChangeDir = Random.Range(1, 5);

            swimX = swimDirectionX * swimSpeed * Time.deltaTime;
            swimZ = swimDirectionZ * swimSpeed * Time.deltaTime;
            
            yield return new WaitForSeconds(timeToChangeDir);
        }
    }


    void Swim()
    {//Debug.Log("Swim: " + swimX + " " + swimZ);
        transform.Translate(swimX, 0f, swimZ);
    }
}
