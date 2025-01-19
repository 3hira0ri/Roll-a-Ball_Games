// Door.cs
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false; // Stan drzwi (otwarte/zamkni�te)
    private Animator animator;   // Animator do obs�ugi animacji drzwi

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
            animator.SetTrigger("OpenDoor"); // Wyzw�l animacj� otwierania
        }
        Debug.Log("Drzwi otwarte.");
    }

    private void CloseDoor()
    {
        isOpen = false;
        if (animator != null)
        {
            animator.SetTrigger("Close"); // Wyzw�l animacj� zamykania
        }
        Debug.Log("Drzwi zamkni�te.");
    }
}
