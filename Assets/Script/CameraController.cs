using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    public float distance = 5.0f; // Pocz¹tkowa odleg³oœæ kamery od celu
    public float sensitivity = 2.0f; // Czu³oœæ obrotu kamery
    public float minYAngle = -20f; // Minimalny k¹t pionowy kamery
    public float maxYAngle = 80f; // Maksymalny k¹t pionowy kamery
    public bool LookAtPlayer = true, inverse = false;
    private float currentX = 0f;
    private float currentY = 20f;

    Vector3 wektor;
    Quaternion wektor2;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wektor = transform.position - player.transform.position;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    private void LateUpdate()
    {
        if (inverse)
        {
            Vector3 direction = wektor;
            Quaternion rotation = Quaternion.Euler(0, -currentX, 0);
            Vector3 desiredPosition = player.transform.position + rotation * direction;

            // Sprawdzenie kolizji za pomoc¹ Raycast
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, desiredPosition - player.transform.position, out hit, distance))
            {
                // Jeœli promieñ trafia w przeszkodê, ustaw kamerê przed przeszkod¹
                transform.position = player.transform.position + (desiredPosition - player.transform.position).normalized * (hit.distance - 0.1f);
            }
            else
            {
                // Jeœli nie ma kolizji, ustaw kamerê na po¿¹danej pozycji
                transform.position = desiredPosition;
            }
            if (LookAtPlayer)
            {
                // Ustawienie, aby kamera by³a skierowana w stronê celu
                transform.LookAt(player.transform);  
                wektor2 = Quaternion.Inverse(transform.rotation);
                transform.rotation = wektor2;
            }
        }
        else
        {
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 desiredPosition = player.transform.position + rotation * direction;

            // Sprawdzenie kolizji za pomoc¹ Raycast
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, desiredPosition - player.transform.position, out hit, distance))
            {
                // Jeœli promieñ trafia w przeszkodê, ustaw kamerê przed przeszkod¹
                transform.position = player.transform.position + (desiredPosition - player.transform.position).normalized * (hit.distance - 0.1f);
            }
            else
            {
                // Jeœli nie ma kolizji, ustaw kamerê na po¿¹danej pozycji
                transform.position = desiredPosition;
            }
            if (LookAtPlayer)
            {
                // Ustawienie, aby kamera by³a skierowana w stronê celu
                transform.LookAt(player.transform);

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        // Pobieranie danych z ruchu myszy
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Ograniczenie k¹ta pionowego kamery
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }
}
