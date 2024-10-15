using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public int score = 0;
    Rigidbody body;
    public float moveUP = 250f;
    public float moveF = 30f;
    float x = 0;
    float y = 0;
    float z = 0;
    public void Whatscore()
    {
        score += 1;
        Debug.Log("zdobyles " + score + " punktow");
        if(score >= 8)
        {
            Debug.Log("Zdobyles wszystkie punkty (nie jest to zbyt duze wyzwanie");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            body.AddForce(x,y,z);
       
    }
    void Update()
    {
        x = 0;
        y = 0;
        z = 0;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position.y <= 0.5)
            {
                y += moveUP;
            }

        }
        if (Input.GetKey(KeyCode.W))
        {
            z += moveF;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z+= -moveF;
        }
        if (Input.GetKey(KeyCode.D))
        {
           x=moveF;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x=-moveF;
        }

    }
}
