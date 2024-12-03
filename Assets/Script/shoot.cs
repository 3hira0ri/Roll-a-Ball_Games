using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    GameObject ammo;
    [SerializeField]
    ParticleSystem flash;
    [SerializeField] float delay;
    GameObject newPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        delay = Random.Range(1f, 2f);

        InvokeRepeating("SpawnAmmo", 0, delay);
    }   
    void SpawnAmmo()
    {
        Vector3 polozenie = new Vector3(0, 1.176f, 0f);
        Quaternion rotacja = new Quaternion(0, 0, 0.707106829f, 0.707106829f);
        flash.Play();

        newPoint = Instantiate(ammo, transform.position, rotacja, transform);
    }

}
