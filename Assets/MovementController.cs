using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;
public class MovementController : MonoBehaviour
{
    public AudioSource AudioSource;
    public event Action pickupEvent;
    public CameraController Kamera;
    Rigidbody body;
    public float moveUP;
    public float moveF;
    float timerJump;
    Vector3 moveDirection;
    public float sprintMultiplier = 2.5f; // Dodatkowy mno¿nik prêdkoœci podczas sprintu
    public AudioClip died;
    private bool alive = true;
    private float hp = 100f;
    public Text tekst;
    private void Awake()
    {
        timerJump = Time.unscaledTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        body.AddForce(moveDirection);
    }
    void Update()
    {
        movement();
    }
    void movement()
    {
        float currentMoveF = moveF;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 forward = Kamera.transform.forward.normalized;
        Vector3 right = Kamera.transform.right.normalized;
        float czas = Time.unscaledTime;
        if (Input.GetKeyDown(KeyCode.Space) && czas - timerJump > 1f)
        {
            body.AddForce(Vector3.up * moveUP, ForceMode.Impulse);
            timerJump = Time.unscaledTime;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentMoveF *= sprintMultiplier;
        }
        // Sprawdzanie nachylenia powierzchni pod kulk¹
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (slopeAngle < 60f) // Dostosuj k¹t w zale¿noœci od potrzeb
            {
                // Mo¿na siê poruszaæ tylko, gdy k¹t jest mniejszy ni¿ 45 stopni
                moveDirection = (forward * vertical + right * horizontal) * currentMoveF;
            }
            else
            {
                // Zatrzymaj poziomy ruch, gdy jest na œcianie
                moveDirection = Vector3.down*moveF;
            }
        }
        
    }
    public bool isalive()
    {
        return alive;
    }
    public void TakeDamage(float damage)
    {
        hp -= damage;
        AudioSource.Play();
        if (hp <= 0)
        {
            death();
        }
    }
    public void restoreHP(float Restored)
    {
        hp += Restored;
    }
    public void death()
    {
        alive = false;
        hp = 0;
        AudioSource.clip = died;
        AudioSource.Play();
        tekst.text = "Przegra³eœ";
        tekst.gameObject.SetActive(true);
        GetComponent<Rigidbody>().isKinematic = true;
        respawn();
    }
    public void respawn()
    {
        transform.position = new Vector3(510.570007f, 0.5f, 972.200012f);
    }
}
