using UnityEngine;

public class StopRotation : MonoBehaviour
{
    Quaternion _startingRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = _startingRotation;
    }
}
