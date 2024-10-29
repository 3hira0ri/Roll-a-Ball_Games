using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
   public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(12.0f * 2 * Time.deltaTime, 12.0f * 2 *  Time.deltaTime, 12.0f * 2 * Time.deltaTime, Space.Self);

    }
    private void turnoff()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider colider)
    {
        colider.gameObject.GetComponent<MovementController>().Whatscore();
        Invoke("turnoff", 1f);
        AudioSource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;


    }
}
