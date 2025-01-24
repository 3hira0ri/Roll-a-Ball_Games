using TMPro;
using UnityEngine;

public class TrapDoor : MonoBehaviour, IInteractable
{           
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 Where;
    [SerializeField] GameObject camera;
    private void Start()
    {
    }

    // Implementacja metody z interfejsu
    public void Interact()
    {

        OpenDoor();
        Invoke("OpenDoor", 1);
    }

    private void OpenDoor()
    {
        Debug.Log("niby dziala");
        Player.transform.position = Where;

    }
}
