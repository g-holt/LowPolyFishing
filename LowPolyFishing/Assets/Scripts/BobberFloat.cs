using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobberFloat : MonoBehaviour
{
    [SerializeField] float amplitude = .5f;
    [SerializeField] float frequency = 1f;

    Vector3 offset;
    Vector3 tempPos;

    public bool isFloating;

    void Start()
    {
        offset = transform.position;
    }

    
    void Update()
    {
        FloatBobber();
    }


    void FloatBobber()
    {
        if(!isFloating) { return; }
    
        tempPos = transform.position;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
