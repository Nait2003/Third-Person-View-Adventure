using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InvetoryManager InvetoryManager;
    public item[] itemsToPickup;


    public void PickupItem(int id)
    {
        bool result = InvetoryManager.AddItem(itemsToPickup[id]);
        if (result == true)
        {
            Debug.Log("ITEM ADDED");
        }
        else
        {
            Debug.Log("Item Not Added");
        }
    }
}
