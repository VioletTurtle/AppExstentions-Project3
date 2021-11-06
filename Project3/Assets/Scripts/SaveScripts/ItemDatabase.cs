using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItem[] items;

    public Dictionary<InventoryItem, int> GetID = new Dictionary<InventoryItem, int>();
    public Dictionary<int, InventoryItem> GetItem = new Dictionary<int, InventoryItem>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<InventoryItem, int>();
        GetItem = new Dictionary<int, InventoryItem>();
        for (int i = 0; i<items.Length; i++)
        {
            GetID.Add(items[i], i);
            GetItem.Add(i, items[i]);
        }
    }

    public void OnBeforeSerialize()
    {}
}
