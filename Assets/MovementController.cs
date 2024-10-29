using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class MovementController : MonoBehaviour
{

    public CameraController Kamera;
    public GameObject Sphere;
    GameObject Player;
    Rigidbody body;
    public float moveUP = 250f;
    public float moveF = 4f;
    float timerJump;
    Vector3 moveDirection;
    public GameObject GameMaster;
    private void Awake()
    {
        timerJump = Time.unscaledTime;
    }
    public void Whatscore()
    {
        GameMaster.GetComponent<NextLevel>().GiveScore();
    }
    // Start is called before the first frame update
    void Start()
    {
        Player = Sphere;
        body = Player.GetComponent<Rigidbody>();
        Kamera.player = Player;
    }

    private void FixedUpdate()
    {
        body.AddForce(moveDirection);
    }
    void Update()
    {
        movement();
       /* if (Input.GetKeyDown(KeyCode.F))
        {
            if(Player == Sphere) {
                Cube.SetActive(true);
                Cube.transform.position = Sphere.transform.position;
                Sphere.SetActive(false);
                Player = Cube;
                body = Player.GetComponent<Rigidbody>();
                camera.player = Player;
            }
            else
            {
                Sphere.transform.position = Cube.transform.position;
                Cube.SetActive(false);
                Sphere.SetActive(true);
                Player = Sphere;
                body = Player.GetComponent<Rigidbody>();
                camera.player = Player;
            }

        }*/
    }
    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = Kamera.transform.forward;
        Vector3 right = Kamera.transform.right;
        float czas = Time.unscaledTime;
        if (Input.GetKeyDown(KeyCode.Space) && czas - timerJump > 1f)
        {
            forward.y = +moveUP;
            timerJump = Time.unscaledTime;
        }
        else
        {
            forward.y = 0;
            right.y = 0;
        }

        moveDirection = (forward * vertical + right * horizontal) * moveF;

        
    }
}
