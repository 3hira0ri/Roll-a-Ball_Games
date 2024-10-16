using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public MovementController playerControler;
    // Start is called before the first frame update
    Vector3 wektor, playerold;
    void Start()
    {
    wektor = transform.position - player.transform.position;
    playerold = player.transform.position;

    }
   
  
    private void FixedUpdate()
    {
        if (player.transform.hasChanged)
        {
            player.transform.hasChanged = false;
            transform.position = player.transform.position + wektor;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
