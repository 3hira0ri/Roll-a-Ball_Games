using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MovementController : MonoBehaviour
{
    public GameObject NextLevelButton;
    public Text TextScore;
    public Text WinText;
    public CameraController camera;
    public GameObject Cube, Sphere;
    GameObject Player;
    public int score = 0;
    Rigidbody body, body2;
    public float moveUP = 250f;
    public float moveF = 30f;
    float x = 0;
    float y = 0;
    float z = 0;
    public void Whatscore()
    {
        score += 1;
        TextScore.text = "Score: " + score;
        if(score >= 8)
        {
           WinText.gameObject.SetActive(true);
           TextScore.gameObject.SetActive(false);
           body.isKinematic = true;
            NextLevelButton.SetActive(true );
           Debug.Log("Zdobyles wszystkie punkty (nie jest to zbyt duze wyzwanie");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Cube.SetActive(false);
        Player = Sphere;
        body = Player.GetComponent<Rigidbody>();
        camera.player = Player;
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
        if (Input.GetKeyDown(KeyCode.F))
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

        }
    }
}
