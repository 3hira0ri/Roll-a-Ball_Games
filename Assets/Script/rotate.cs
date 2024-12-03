using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    bool flagay, flagaz, flagax = false,flago = false;
    float degreesPerSecond = 3;
    // Start is called before the first frame update
    void Start()
    {
        //flagay = false; flagaz = false; flagax = false;
    }
    
    void FixedUpdate()
    {

        float stopnie = transform.eulerAngles.x;
        
        if (stopnie > 15 && stopnie < 345 && !flago)
        {
            flagax = true;
        }
        if (stopnie >= 0 && stopnie <= 345 && flago)
        {
            flagax = false;
        }
        
        if (flagax == false)
        {
            Debug.Log("przod");
            transform.Rotate(new Vector3(degreesPerSecond, 0, 0) * Time.deltaTime);
        }
        else if(flagax == true)
        {
            Debug.Log("tyl");
            transform.Rotate(new Vector3(-degreesPerSecond, 0, 0) * Time.deltaTime);
        }
        if (2<=stopnie & stopnie >= -2)
        {
            if (flago)
            {
                flago = true;
            }else
            {
                flago = false;
            }

        }
            //y
            /*if (transform.rotation.y <= 0)
            {
                flagax = false;
            }
            if (transform.rotation.y >= 0.1305262)
            {
                flagax = true;
            }
            if (transform.rotation.y < 0.1305262 && flagay == false)
            {
                transform.Rotate( 0, 12.0f * 2 * Time.deltaTime, 0, Space.World);
            }
            else if (transform.rotation.y > 0.1305262 && flagay == true)
            {
                transform.Rotate( 0, -12.0f * 2 * Time.deltaTime, 0, Space.World);
            }*/
            //z
            /* if (transform.rotation.z <= 0)
             {
                 flagax = false;
             }
             if (transform.rotation.z >= 0.1305262)
             {
                 flagax = true;
             }
             if (transform.rotation.z < 0.1305262 && flagaz == false)
             {
                 transform.Rotate( 0, 0, 12.0f * 2 * Time.deltaTime, Space.World);
             }
             else if (transform.rotation.z > 0.1305262 && flagaz == true)
             {
                 transform.Rotate( 0, 0, -12.0f * 2 * Time.deltaTime, Space.World);
             }*/
            Debug.Log("FLaga: "+flagax+flago+" koordynaty wynosz¹ " + stopnie + " " + transform.rotation.y + " " + transform.rotation.z);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
