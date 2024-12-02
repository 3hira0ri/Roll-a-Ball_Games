using UnityEngine;

public class firework : MonoBehaviour
{
    [SerializeField] GameObject ammo;
    float speed = 300;
    public static firework Instance { get; private set; }
        private void Awake()
        {
            Invoke("DestroyFirework", 1);
        }
    void DestroyFirework()
    {
        
            GameObject effect = Instantiate(ammo, transform.position, Quaternion.identity, transform);

       

        Destroy(this.gameObject,0.4f);
    }
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.up * speed);
        Debug.Log("niby dzia³a");
    }
}

