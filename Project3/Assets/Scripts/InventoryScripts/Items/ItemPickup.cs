using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private ItemSlot itemSlot;
    [SerializeField] private GameObject ui;
    private int qunatity = 0;

    public void OnTriggerEnter(Collider other)
    {
        var itemContainer = other.GetComponent<IItemContainer>();

        if (itemContainer == null) { return; }

        int Moved = itemContainer.AddItem(itemSlot).quantity;
        if (Moved == 0)
        {
            Destroy(this.gameObject);
        }
        else if (Moved != 0 && itemSlot.quantity != (qunatity - itemContainer.GetTotalQuantity(itemSlot.item)))
        {
            GameObject instantiated = Instantiate(ui);
            Destroy(instantiated, 2f);
            itemSlot.quantity -= itemContainer.GetTotalQuantity(itemSlot.item);
        }
        else if(itemSlot.quantity == qunatity)
        {
            GameObject instantiated = Instantiate(ui);
            Destroy(instantiated, 2f);
        }
    }

    private void Start()
    {
        qunatity = itemSlot.quantity;
    }
}