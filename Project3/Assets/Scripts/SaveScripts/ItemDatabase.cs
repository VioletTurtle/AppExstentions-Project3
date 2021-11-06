using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Items/Item Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public InventoryItem[] items;

    public Dictionary<InventoryItem, int> GetID = new Dictionary<InventoryItem, int>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<InventoryItem, int>();
        for (int i = 0; i<items.Length; i++)
        {
            GetID.Add(items[i], i);
        }
    }

    public void OnBeforeSerialize()
    {}
}
