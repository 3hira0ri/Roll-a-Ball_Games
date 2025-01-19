using System;
using System.Collections.Generic;
using UnityEngine;

public class LeverActive : MonoBehaviour, IInteractable
{
    [SerializeField] private Animation animator;   // Animator do obs³ugi animacji switch
    [SerializeField] DynamicInventory CraftingInventory;
    [SerializeField] List<GameObject> SlimesBig;
    // Implementacja metody z interfejsu
    public void Interact()
    {
        SwitchON();
        CraftSlime();
    }

    private void CraftSlime()
    {
        string name = null;
        if(CraftingInventory!=null && CraftingInventory.items.Count>=2) {
           
            foreach (var item in CraftingInventory.items)
            {
                if (name == null)
                {
                    Debug.Log("cos respi2");
                    name = item.item.name;
                }
                else
                {

                    if (name == item.item.name)
                    {

                        Vector3 where = transform.position;
                        where.z += 1;
                        foreach (var slime in SlimesBig)
                        {
                            Debug.Log(item.item.name + "Big2");
                            Debug.Log(slime.name);
                            if (item.item.name + "Big" == slime.name)
                            {

                                GameObject droppedItem = Instantiate(slime, where, Quaternion.identity);
                                CraftingInventory.items.Clear();

                            }
                        }
                    }
                }
            }

        }
    }

    private void SwitchON()
    {

        if (animator != null)
        {
            Debug.Log("wesz³o");
            animator.Play(); // Wyzwól animacjê switcha
        }
    }

}
