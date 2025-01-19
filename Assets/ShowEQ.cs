using UnityEngine;
using UnityEngine.EventSystems;

public class ShowEQ : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panelUI;

    public void OnPointerClick(PointerEventData eventData)
    {
        // SprawdŸ, czy klikniêcie by³o lewym przyciskiem myszy
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("CLICK: " + gameObject.GetComponent<ItemDisplay>().itemIndex);    
            int index = gameObject.GetComponent<ItemDisplay>().itemIndex;
            ItemData item = panelUI.GetComponent<InventoryDisplay>().inventory.items[index].item;
            Vector3 where = GameObject.FindGameObjectWithTag("Player").transform.position;
            where.z += 5;
            panelUI.GetComponent<InventoryDisplay>().inventory.DropItem(item,where);
            panelUI.GetComponent<InventoryDisplay>().inventory.RemoveItem(item);
            panelUI.GetComponent<InventoryDisplay>().UpdateInventory();
        }
    }
}
