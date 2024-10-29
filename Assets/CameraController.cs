using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public MovementController playerControler;
    public Transform target; // Obiekt, wok� kt�rego kamera si� obraca (np. kula gracza)

    public float distance = 5.0f; // Pocz�tkowa odleg�o�� kamery od celu
    public float sensitivity = 2.0f; // Czu�o�� obrotu kamery (im wi�ksza, tym szybszy obr�t)
    public float minYAngle = -20f; // Minimalny k�t pionowy kamery
    public float maxYAngle = 80f; // Maksymalny k�t pionowy kamery

    private float currentX = 0f; // Aktualny k�t obrotu wok� osi X
    private float currentY = 20f; // Aktualny k�t obrotu wok� osi Y
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
    { // Ustawienie pozycji kamery wzgl�dem celu
        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = target.position + rotation * direction;

        // Ustawienie, aby kamera by�a skierowana w stron� celu
        transform.LookAt(target);
    }
    // Update is called once per frame
    void Update()
    {
        // Pobieranie danych z ruchu myszy
        currentX += Input.GetAxis("Mouse X") * sensitivity;
        currentY -= Input.GetAxis("Mouse Y") * sensitivity;

        // Ograniczenie k�ta pionowego kamery
        currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
    }
}
