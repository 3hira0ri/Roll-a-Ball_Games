using UnityEngine;

public class TrapDoor : MonoBehaviour, IInteractable
{           
    [SerializeField] GameObject Player;
    [SerializeField] Vector3 Where;
    private void Start()
    {
    }

    // Implementacja metody z interfejsu
    public void Interact()
    {

            OpenDoor();

    }

    private void OpenDoor()
    {
        Debug.Log("niby dziala");
        Player.transform.position = Where;
    }
}
