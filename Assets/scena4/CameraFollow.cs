using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player; 
     Vector3 offset;   

    void Awake()
    {
        Debug.Log(player.transform.position);
        Debug.Log(transform.position);
        offset =  player.transform.position- transform.position ;
        Debug.Log(offset);
    }
    void LateUpdate()
    {
        transform.position = player.transform.position - offset;
    }
}
