using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    GameObject ammo;
    [SerializeField]
    ParticleSystem flash;
    GameObject newPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        InvokeRepeating("SpawnAmmo", 0, 2.5f);
        
    }
    void SpawnAmmo()
    {
        Vector3 polozenie = new Vector3(0, 1.176f, 0f);
        Quaternion rotacja = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
        flash.Play();
        newPoint = Instantiate(ammo, transform.position, rotacja, transform);
    }

}
