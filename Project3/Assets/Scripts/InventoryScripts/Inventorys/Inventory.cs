using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour, IItemContainer, ISerializationCallbackReceiver
{
    [SerializeField] private int size = 12;
    public ItemDatabase database;
    private List<ItemSlot> itemSlotsList = new List<ItemSlot>();
    private ItemSlot[] itemSlots = new ItemSlot[0];
    [SerializeField] private UnityEvent onInventoryItemsUpdated = null;
    public string level;

    private void Start()
    {
        itemSlots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            itemSlotsList.Add(new ItemSlot());
        }
        if (SaveSystem.Instance.isNew == true)
        {
            SaveInventory();
        }
        LoadInventory();
    }

    public ItemSlot GetSlotByIndex(int index) => itemSlotsList[index];

    public ItemSlot AddItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if (itemSlotsList[i].item != null)
            {
                if (itemSlotsList[i].item == itemSlot.item)
                {
                    int slotRemainingSpace = itemSlotsList[i].item.MaxStack - itemSlots[i].quantity;

                    if (itemSlot.quantity <= slotRemainingSpace)
                    {
                        itemSlots[i].quantity += itemSlot.quantity;
                        itemSlotsList[i]= new ItemSlot(itemSlots[i].item, itemSlots[i].quantity, database.GetID[itemSlots[i].item]);

                        itemSlot.quantity = 0;

                        onInventoryItemsUpdated.Invoke();

                        return itemSlot;
                    }
                    else if (slotRemainingSpace > 0)
                    {
                        itemSlots[i].quantity += slotRemainingSpace;
                        itemSlotsList[i] = new ItemSlot(itemSlots[i].item, itemSlots[i].quantity, database.GetID[itemSlots[i].item]);

                        itemSlot.quantity -= slotRemainingSpace;
                    }
                }
            }
        }

        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if (itemSlots[i].item == null)
            {
                if (itemSlot.quantity <= itemSlot.item.MaxStack)
                {
                    itemSlots[i] = itemSlot;
                    itemSlotsList[i] = new ItemSlot(itemSlot.item, itemSlot.quantity, database.GetID[itemSlot.item]);

                    itemSlot.quantity = 0;

                    onInventoryItemsUpdated.Invoke();

                    return itemSlot;
                }
                else
                {
                    itemSlots[i] = itemSlot;
                    itemSlotsList[i] = new ItemSlot(itemSlots[i].item, itemSlots[i].quantity, database.GetID[itemSlots[i].item]);

                    itemSlot.quantity -= itemSlot.item.MaxStack;
                }
            }
        }

        onInventoryItemsUpdated.Invoke();

        return itemSlot;
    }

    public void UpdateQuantity(InventoryItem item)
    {
        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if (itemSlotsList[i].item != null)
            {
                if (itemSlotsList[i].item == item)
                {
                    itemSlots[i].quantity -= 1;
                    itemSlotsList[i] = new ItemSlot(itemSlots[i].item, itemSlots[i].quantity, database.GetID[itemSlots[i].item]);

                    if (itemSlots[i].quantity == 0)
                    {
                        RemoveItem(itemSlotsList[i]);
                        return;
                    }

                    onInventoryItemsUpdated.Invoke();
                    return;
                }
            }
        }
    }
    public void RemoveItem(ItemSlot itemSlot)
    {
        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if (itemSlotsList[i].item != null)
            {
                if (itemSlotsList[i].item == itemSlot.item)
                {
                    if (itemSlotsList[i].quantity < itemSlot.quantity)
                    {
                        itemSlot.quantity -= itemSlots[i].quantity;

                        itemSlots[i] = new ItemSlot();
                        itemSlotsList[i] = new ItemSlot();
                    }
                    else
                    {
                        itemSlots[i].quantity -= itemSlot.quantity;
                        itemSlotsList[i] = new ItemSlot(itemSlots[i].item, itemSlots[i].quantity, database.GetID[itemSlots[i].item]);

                        if (itemSlotsList[i].quantity == 0)
                        {
                            itemSlots[i] = new ItemSlot();
                            itemSlotsList[i] = new ItemSlot();

                            onInventoryItemsUpdated.Invoke();

                            return;
                        }
                    }
                }
            }
        }
    }

    public void RemoveAt(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex > itemSlotsList.Count - 1) { return; }

        itemSlots[slotIndex] = new ItemSlot();
        itemSlotsList[slotIndex] = new ItemSlot();

        onInventoryItemsUpdated.Invoke();
    }

    public void Swap(int indexOne, int indexTwo)
    {
        ItemSlot firstSlot = itemSlotsList[indexOne];
        ItemSlot secondSlot = itemSlotsList[indexTwo];

        if (firstSlot == secondSlot) { return; }

        if (secondSlot.item != null)
        {
            if (firstSlot.item == secondSlot.item)
            {
                int secondSlotRemainingSpace = secondSlot.item.MaxStack - secondSlot.quantity;

                if (firstSlot.quantity <= secondSlotRemainingSpace)
                {
                    itemSlots[indexTwo].quantity += firstSlot.quantity;
                    itemSlotsList[indexTwo] = new ItemSlot(itemSlots[indexTwo].item, itemSlots[indexTwo].quantity, database.GetID[itemSlots[indexTwo].item]);

                    itemSlots[indexOne] = new ItemSlot();
                    itemSlotsList[indexOne] = new ItemSlot(itemSlots[indexOne].item, itemSlots[indexOne].quantity, database.GetID[itemSlots[indexOne].item]);

                    onInventoryItemsUpdated.Invoke();

                    return;
                }
            }
        }

        itemSlots[indexOne] = secondSlot;
        itemSlotsList[indexOne] = secondSlot;
        itemSlots[indexTwo] = firstSlot;
        itemSlotsList[indexTwo] = firstSlot;

        onInventoryItemsUpdated.Invoke();
    }

    public bool HasItem(InventoryItem item)
    {
        foreach (ItemSlot itemSlot in itemSlotsList)
        {
            if (itemSlot.item == null) { continue; }
            if (itemSlot.item != item) { continue; }

            return true;
        }

        return false;
    }

    public int GetTotalQuantity(InventoryItem item)
    {
        int totalCount = 0;

        foreach (ItemSlot itemSlot in itemSlotsList)
        {
            if (itemSlot.item == null) { continue; }
            if (itemSlot.item != item) { continue; }

            totalCount += itemSlot.quantity;
        }

        return totalCount;
    }
    public int GetItemAmount()
    {
        int total = 0;
        foreach(ItemSlot item in itemSlots)
        {
            if(item.quantity != 0) { total++; }
        }
        return total;
    }

    public void SaveInventory()
    {
        SaveSystem.Instance.SaveInventory(itemSlotsList);
        SaveSystem.Instance.level = level;
    }
    public void LoadInventory()
    {
        itemSlotsList = SaveSystem.Instance.LoadInventory(itemSlotsList);
        for (int i = 0; i < itemSlotsList.Count; i++)
        {
            if(itemSlotsList[i].item == null)
            {
                itemSlotsList[i] = new ItemSlot();
            }

            itemSlots[i] = itemSlotsList[i];
        }
        onInventoryItemsUpdated.Invoke();
    }

    public void OnBeforeSerialize()
    {}

    public void OnAfterDeserialize()
    {
        for(int i =0; i< itemSlotsList.Count; i++)
        {
            itemSlots[i].item = database.GetItem[itemSlotsList[i].ID];
            itemSlotsList[i] = itemSlots[i];
        }
    }  
}
