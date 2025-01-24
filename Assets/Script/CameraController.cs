using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode
    {
        ThirdPerson,
        FirstPerson
    }

    [Header("Camera Settings")]
    public CameraMode SelectedMode = CameraMode.ThirdPerson; // Wybór trybu kamery w edytorze

    private GameObject _player;

    [Header("Third-Person Settings")]
    public float Distance = 5.0f; // Odleg³oœæ kamery od gracza w trybie trzecioosobowym
    public float Sensitivity = 2.0f; // Czu³oœæ obrotu kamery
    public float MinYAngle = -20f; // Minimalny k¹t pionowy kamery
    public float MaxYAngle = 120f; // Maksymalny k¹t pionowy kamery
    public bool LookAtPlayer = true;

    [Header("First-Person Settings")]
    public Vector3 FirstPersonOffset = new Vector3(0f, 1.6f, 0f); // Offset kamery w trybie pierwszoosobowym

    private float _currentX = 0f;
    private float _currentY = 20f;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        if (SelectedMode == CameraMode.FirstPerson)
        {
            HandleFirstPersonCamera();
        }
        else if (SelectedMode == CameraMode.ThirdPerson)
        {
            HandleThirdPersonCamera();
        }
    }

    private void Update()
    {
        // Pobieranie danych z ruchu myszy
        _currentX += Input.GetAxis("Mouse X") * Sensitivity;
        _currentY -= Input.GetAxis("Mouse Y") * Sensitivity;

        // Ograniczenie k¹ta pionowego kamery
        _currentY = Mathf.Clamp(_currentY, MinYAngle, MaxYAngle);
    }

    private void HandleFirstPersonCamera()
    {
        // Kamera umieszczona w pozycji gracza z offsetem
        transform.position = _player.transform.position + FirstPersonOffset;
        transform.rotation = Quaternion.Euler(_currentY, _currentX, 0);
    }

    private void HandleThirdPersonCamera()
    {
        Vector3 direction = new Vector3(0, 0, -Distance);
        Quaternion rotation = Quaternion.Euler(_currentY, _currentX, 0);
        Vector3 desiredPosition = _player.transform.position + rotation * direction;

        // Sprawdzenie kolizji za pomoc¹ Raycast
        RaycastHit hit;
        if (Physics.Raycast(_player.transform.position, desiredPosition - _player.transform.position, out hit, Distance))
        {
            // Jeœli promieñ trafia w przeszkodê, ustaw kamerê przed przeszkod¹
            transform.position = _player.transform.position + (desiredPosition - _player.transform.position).normalized * (hit.distance - 0.1f);
        }
        else
        {
            // Jeœli nie ma kolizji, ustaw kamerê na po¿¹danej pozycji
            transform.position = desiredPosition;
        }

        if (LookAtPlayer)
        {
            // Ustawienie, aby kamera by³a skierowana w stronê gracza
            transform.LookAt(_player.transform);
        }
    }
    public void SyncCameraWithPlayer()
    {
        if (SelectedMode == CameraMode.FirstPerson)
        {
            transform.position = _player.transform.position + FirstPersonOffset;
        }
        else if (SelectedMode == CameraMode.ThirdPerson)
        {
            HandleThirdPersonCamera();
        }
    }
}
