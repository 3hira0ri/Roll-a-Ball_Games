using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(12.0f * 2 * Time.deltaTime, 12.0f * 2 *  Time.deltaTime, 12.0f * 2 * Time.deltaTime, Space.Self);

    }
    private void OnTriggerEnter(Collider colider)
    {
       
        gameObject.SetActive(false);
        colider.gameObject.GetComponent<MovementController>().Whatscore();

    }
}
