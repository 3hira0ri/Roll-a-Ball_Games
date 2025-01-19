// Door.cs
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false; // Stan drzwi (otwarte/zamkniête)
    private Animator animator;   // Animator do obs³ugi animacji drzwi

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Implementacja metody z interfejsu
    public void Interact()
    {

        
            OpenDoor();


    }

    private void OpenDoor()
    {
        isOpen = true;
        if (animator != null)
        {
            animator.SetTrigger("OpenDoor"); // Wyzwól animacjê otwierania
        }
        Debug.Log("Drzwi otwarte.");
    }

    private void CloseDoor()
    {
        isOpen = false;
        if (animator != null)
        {
            animator.SetTrigger("Close"); // Wyzwól animacjê zamykania
        }
        Debug.Log("Drzwi zamkniête.");
    }
}
