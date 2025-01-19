using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemDisplay : MonoBehaviour
{
    public int itemIndex;
    public UnityEngine.UI.Image image;
    public Text stackCountText;

    public void UpdateItemDisplay(Sprite newSprite, int newItemIndex, int stackCount)
    {
        image.sprite = newSprite;
        itemIndex = newItemIndex;
        stackCountText.text = stackCount > 1 ? "x2" : "x1"; // Wy�wietla �x2� tylko dla wi�cej ni� jednego przedmiotu
    }


}