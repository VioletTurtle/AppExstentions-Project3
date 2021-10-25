using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private List<HotbarSlot> hotbarSlotsList = new List<HotbarSlot>();
    private HotbarSlot[] hotbarSlots = new HotbarSlot[4];
    [SerializeField] private GameObject Player = null;

    public void Add(HotbarItem itemToAdd)
    {
        foreach(HotbarSlot hotbarSlot in hotbarSlotsList )
        {
            if (hotbarSlot.AddItem(itemToAdd)) { return; }
        }
    }

    private void Start()
    {
        hotbarSlots = GetComponentsInChildren<HotbarSlot>();
        for (int i = 0; i < 4; i++)
        {
            hotbarSlotsList.Add(hotbarSlots[i]);
        }
    }

    void Update()
    {
        hotbarSlots = GetComponentsInChildren<HotbarSlot>();
        for (int i = 0; i < 4; i++)
        {
            hotbarSlotsList[i] = hotbarSlots[i];
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hotbarSlotsList[0].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotbarSlotsList[1].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hotbarSlotsList[2].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hotbarSlotsList[3].UseSlot(Player);
        }
    }
}
