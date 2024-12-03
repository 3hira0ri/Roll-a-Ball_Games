using UnityEngine;

public class firework : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    AudioSource AudioSource;
    float speed = 300;
    public static firework Instance { get; private set; }
        private void Awake()
        {
            Invoke("DestroyFirework", 1);
        }
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
    void DestroyFirework()
    {
        
            GameObject effect = Instantiate(ammo, transform.position, Quaternion.identity, transform);
            AudioSource.Play();
            speed = 0;

            Destroy(this.gameObject,0.8f);
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * speed);
        Debug.Log("niby dzia³a");
    }
}

