using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManager : MonoBehaviour
{
    public Inventory_slot[] inventory_Slots;
    public GameObject iventoryItemPrefab;

    public bool AddItem(item item)
    {
        for (int i = 0; i < inventory_Slots.Length; i++)
        {
           Inventory_slot slot = inventory_Slots[i];
           Draggable itemInSlot = slot.GetComponentInChildren<Draggable>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }
        return false;
    }

    void SpawnNewItem(item item, Inventory_slot slot)
    {
        GameObject newItemGo = Instantiate(iventoryItemPrefab, slot.transform);
        Draggable draggable = newItemGo.GetComponent<Draggable>();
        draggable.InitialiseItem(item);
    }
}
