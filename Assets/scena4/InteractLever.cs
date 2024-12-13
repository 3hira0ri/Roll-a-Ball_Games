using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class InteractLever : MonoBehaviour, IInteract
{
    [SerializeField] GameObject whatInteracted;
    public void interact()
    {
       gameObject.GetComponent<Animation>().Play();
       whatInteracted.gameObject.SetActive(true);

    }
    private void OnEnable()
    {
        Movement.InteractEvent += HandleInteract;
    }

    private void OnDisable()
    {
        Movement.InteractEvent -= HandleInteract;
    }
    private void HandleInteract(IInteract interactable)
    {
        if (interactable == this)
        {
            interact();
        }
    }
}
