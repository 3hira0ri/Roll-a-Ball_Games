using UnityEngine;
using UnityEngine.EventSystems;

public class PointerClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] bool IsPlayer;
    [SerializeField] GameObject panelFROM;
    [SerializeField] GameObject panelTO;

    public void OnPointerClick(PointerEventData eventData)
    {
        // Sprawd�, czy klikni�cie by�o lewym przyciskiem myszy
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int index = gameObject.GetComponent<ItemDisplay>().itemIndex;
            ItemData item = panelFROM.GetComponent<InventoryDisplay>().inventory.items[index].item;
            if(panelTO.GetComponent<InventoryDisplay>().inventory.AddItem(item))
            panelFROM.GetComponent<InventoryDisplay>().inventory.RemoveItem(item);
            panelTO.GetComponent<InventoryDisplay>().UpdateInventory();
            panelFROM.GetComponent<InventoryDisplay>().UpdateInventory();
        }
    }
}
