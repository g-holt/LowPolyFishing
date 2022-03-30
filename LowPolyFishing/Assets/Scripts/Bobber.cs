using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField] float castStrength = 5f;


    //bobber Prefab is visible or not based on pole visibility, so don't have to SetActive() it independently
    public void ThrowLine()
    {
        Transform player = GetComponentInParent<Transform>();
        Debug.Log("Line Out: Throw Bobber");
        transform.Translate(player.forward * castStrength, Space.Self);
    }

}