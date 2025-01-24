using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] Canvas PlayerUI;              // UI gracza
    [SerializeField] Canvas ContentDisplay;        // UI wy�wietlaj�ce zawarto�� skrzyni
    [SerializeField] InventoryDisplay InventoryDisplay;
    [SerializeField] InventoryDisplay PlayerDisplay;
    [SerializeField] InventoryDisplay EQ;
    [SerializeField] PlayerInventory PlayerINV;      // Zawarto�� skrzyni
    [SerializeField] GameObject Player;            // Gracz
    [SerializeField] DynamicInventory Content;
    [SerializeField] GameObject gun;
    bool IsOpened = false;

    private void Start()
    {
        PlayerUI.gameObject.SetActive(true);
    }

    public void Interact()
    {
        ShowContent();
    }

    private void ShowContent()
    {
        if (!IsOpened)
        {
            // Zablokowanie ruchu gracza i otwarcie skrzyni
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.GetComponent<MovementController>().enabled=false;
            gun.GetComponent<Gun>().enabled = false;
            IsOpened = true;
            PlayerUI.gameObject.SetActive(false);
            ContentDisplay.gameObject.SetActive(true);
            InventoryDisplay.inventory = Content;
            InventoryDisplay.UpdateInventory();
            PlayerDisplay.UpdateInventory();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Debug.Log("Skrzynia otwarta.");
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
            // Zamkni�cie skrzyni i przywr�cenie kontroli nad graczem
            PlayerUI.gameObject.SetActive(true);
            EQ.UpdateInventory();
            ContentDisplay.gameObject.SetActive(false);
            gun.GetComponent<Gun>().enabled = true;
            IsOpened = false;
            Player.GetComponent<Rigidbody>().isKinematic = false;
            Player.GetComponent<MovementController>().enabled = true;
            Debug.Log("Skrzynia zamkni�ta.");
        }
    }


    private void Update()
    {
        // Umo�liwienie zamkni�cia skrzyni przez naci�ni�cie 'E'
        if (IsOpened && Input.GetKeyDown(KeyCode.E))
        {
            ShowContent(); // Zamknij skrzyni�
        }
    }


}
