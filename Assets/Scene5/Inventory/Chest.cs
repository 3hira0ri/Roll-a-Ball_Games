using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;  // Do pracy z UI

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

    // Funkcja do wykonania akcji na przedmiocie
    /*private void OnItemClicked(Button clickedButton)
    {

        // Tutaj mo�esz wykona� odpowiedni� akcj� dla przedmiotu
        // np. doda� go do ekwipunku gracza
        Debug.Log("Przedmiot klikni�ty: " + clickedButton.name);
        // Zak�adaj�c, �e masz metod� w PlayerInventory do dodawania przedmiotu:
        foreach(ItemSlot Item in Content.items)
        {
            if(Item.item.itemName == clickedButton.name)
            {
                if (Item.count == 1) { 
                Content.items.Remove(Item);
               // PlayerINV.inventory.AddItem(Item.item.itemType, Item.item.model);
                return;
            }else if(Item.count == 2)
                {
                    //Content.inventory.items.;
                    PlayerINV.inventory.items.Add(Item);
                    return;
                }
            }
        }
        //PlayerINV.inventory.AddItem(itemInstance, itemObject);
    }
*/
    private void Update()
    {
        // Umo�liwienie zamkni�cia skrzyni przez naci�ni�cie 'E'
        if (IsOpened && Input.GetKeyDown(KeyCode.E))
        {
            ShowContent(); // Zamknij skrzyni�
        }
    }


}
