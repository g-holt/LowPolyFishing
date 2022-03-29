using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float castStrength = 5f;


    //bobber Prefab is visible or not based on pole visibility, so don't have to SetActive() it independently
    public void ThrowLine()
    {
        Debug.Log("Line Out: Throw Bobber");
        transform.Translate(castStrength, 0, castStrength, Space.Self);
    }

}