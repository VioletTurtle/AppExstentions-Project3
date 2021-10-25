using TMPro;
using UnityEngine;

public class ItemDestroyer : MonoBehaviour
{
    [SerializeField] Inventory inventory = null;
    [SerializeField] TextMeshProUGUI areYouSureText = null;

    private int slotIndex = 0;

    private void OnDisable() => slotIndex = -1;

    public void Activate(ItemSlot itemSlot, int slotIndex)
    {
        this.slotIndex = slotIndex;
        areYouSureText.text = $"Are you sure you wish to destroy {itemSlot.quantity}x {itemSlot.item.ColouredName}?";
        gameObject.SetActive(true);
    }

    public void Destroy()
    {
        inventory.RemoveAt(slotIndex);
        gameObject.SetActive(false);
    }


}
