using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ParticleSystem _lance; // Prefab pocisku
    [Header("Fire Settings")]
    [SerializeField] private float fireRate = 2; // how fast shoot
    private float fireCooldown = 0f;

    private void Update()
    {
        // Obs�uga strza�u
        fireCooldown -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && fireCooldown <= 0f) // LPM lub Fire1
        {
            Fire();
            fireCooldown = fireRate;
        }
    }

    private void Fire()
    {
        _lance.Play();
    }
}
