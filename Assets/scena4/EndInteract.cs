using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndInteract : MonoBehaviour, IInteract
{
    [SerializeField] GameObject[] whatInteracted;
    [SerializeField] StartGame game;
    public void interact()
    {

        whatInteracted[0].gameObject.SetActive(true);
        whatInteracted[1].gameObject.SetActive(false);
        Invoke("WON", 3f);
        Invoke("StartGame", 4f);
    }
    void WON()
    {
        whatInteracted[2].gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        Movement.InteractEvent += HandleInteract;
    }
    void StartGame()
    {
        game.ChangeLevel(4);
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
