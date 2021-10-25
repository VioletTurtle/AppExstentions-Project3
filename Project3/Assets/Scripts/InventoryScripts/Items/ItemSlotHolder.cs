using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotHolder : MonoBehaviour
{
    [SerializeField] private Inventory inventory = null;
    public GameObject[] slots;
    int activeAmount = 0;

    private void Update()
    {
        activeAmount = 0;
        if (inventory.GetItemAmount() <= 6)
        {

            for(int i = 0; i < 12; i++)
            {
                if(slots[i].GetComponent<InventorySlot>().ItemSlot.quantity > 0)
                {
                    activeAmount++;
                    slots[i].SetActive(true);
                }
                if (slots[i].GetComponent<InventorySlot>().ItemSlot.quantity == 0)
                {
                    slots[i].SetActive(false);
                }
            }
            for (int i = 0; i < 12; i++)
            {
                if(activeAmount < 6)
                {
                    if(slots[i].activeSelf == false)
                    {
                        activeAmount++;
                        slots[i].SetActive(true);
                    }
                }
            }
        }
        if(inventory.GetItemAmount() > 6)
        {
            for (int i = 0; i < 12; i++)
            {
                if (slots[i].GetComponent<InventorySlot>().ItemSlot.quantity > 0)
                {
                    slots[i].SetActive(true);
                }
                else
                {
                    slots[i].SetActive(false);
                }
            }
        }
    }
}
