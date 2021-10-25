using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable Item", menuName = "Items/Consumable Item")]
public class ConsumableItem : InventoryItem, IHotbarItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useText = "Does something, maybe?";
    [SerializeField] private int healthAddAmount = 1;

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Rarity.Name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        return builder.ToString();
    }

    public override void Use(GameObject player)
    {
        HealthSystem hs = player.GetComponent<HealthSystem>();
        if (hs != null)
        {
            if (hs.Health < 100)
            {
                hs.Heal(healthAddAmount);
                Debug.Log($"Drinking {Name}; Health {hs.Health}");
                if(hs.Health > 100)
                {
                    hs.ResetHealth();
                }
            }
        }
    }
}
