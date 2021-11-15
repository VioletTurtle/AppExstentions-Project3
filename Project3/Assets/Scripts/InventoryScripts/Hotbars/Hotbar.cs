using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    private List<HotbarSlot> hotbarSlotsList = new List<HotbarSlot>();
    private HotbarSlot[] hotbarSlots = new HotbarSlot[4];
    private HotbarSlot slotToUse;
    [SerializeField] private GameObject Player = null;
    public bool isInventory = false;

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

        slotToUse = hotbarSlotsList[0];
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
            slotToUse = hotbarSlotsList[0];
            //hotbarSlotsList[0].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slotToUse = hotbarSlotsList[1];
            //hotbarSlotsList[1].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slotToUse = hotbarSlotsList[2];
            //hotbarSlotsList[2].UseSlot(Player);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slotToUse = hotbarSlotsList[3];
            //hotbarSlotsList[3].UseSlot(Player);
        }


        if(Input.GetKeyDown(KeyCode.Mouse0) && isInventory == false)
        {
            slotToUse.UseSlot(Player);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Player.GetComponent<Inventory>().SaveInventory();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Player.GetComponent<Inventory>().LoadInventory();
        }
    }
}
