using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Collectible : MonoBehaviour
{
    public AudioSource AudioSource;
    public event Action PickUpEvent;
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
        PickUpEvent?.Invoke();
        Invoke("turnoff", 1f);
        AudioSource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;


    }
}
