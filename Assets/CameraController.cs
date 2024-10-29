using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public MovementController playerControler;
    public Transform target; // Obiekt, wokó³ którego kamera siê obraca (np. kula gracza)

    public float distance = 5.0f; // Pocz¹tkowa odleg³oœæ kamery od celu
    public float sensitivity = 2.0f; // Czu³oœæ obrotu kamery (im wiêksza, tym szybszy obrót)
    public float minYAngle = -20f; // Minimalny k¹t pionowy kamery
    public float maxYAngle = 80f; // Maksymalny k¹t pionowy kamery

    private float currentX = 0f; // Aktualny k¹t obrotu wokó³ osi X
    private float currentY = 20f; // Aktualny k¹t obrotu wokó³ osi Y
    // Start is called before the first frame update
    Vector3 wektor, playerold;
    void Start()
    {
        target = player.transform;
        wektor = transform.position - player.transform.position;
        playerold = player.transform.position;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }


    private void LateUpdate()
    { // Ustawienie pozycji kamery wzglêdem celu
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * direction;

        // Ustawienie, aby kamera by³a skierowana w stronê celu
        transform.LookAt(target);
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
