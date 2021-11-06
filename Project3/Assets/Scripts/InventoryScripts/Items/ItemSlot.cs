using System;

[Serializable]
public struct ItemSlot
{
    public InventoryItem item;
    public int quantity;
    public int ID;

    public ItemSlot(InventoryItem item, int quantity, int ID)
    {
        this.ID = ID;
        this.item = item;
        this.quantity = quantity;
    }

    public static bool operator ==(ItemSlot a, ItemSlot b) { return a.Equals(b); }

    public static bool operator !=(ItemSlot a, ItemSlot b) { return !a.Equals(b); }
}