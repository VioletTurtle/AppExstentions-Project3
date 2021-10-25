using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Item", menuName = "Items/Armor Item")]
public class ArmorItem : InventoryItem, IHotbarItem
{
    [Header("Armor Data")]
    [SerializeField] private string useText = "Does something, maybe?";
    [SerializeField] private float armorAmount = 0.1f;

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Rarity.Name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();
        builder.Append("Max Armor: 5").AppendLine();

        return builder.ToString();
    }

    public override void Use(GameObject player)
    {
        HealthSystem hs = player.GetComponent<HealthSystem>();
        if (hs != null)
        {
            if (hs.armor < 5)
            {
                hs.armor += armorAmount;
                if(hs.armor > 5)
                {
                    hs.armor = 5;
                }
            }
        }
    }
}
