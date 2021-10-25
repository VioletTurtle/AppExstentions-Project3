using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition Item", menuName = "Items/Ammunition Item")]
public class AmmunitionItem : InventoryItem, IHotbarItem
{
    [Header("Consumable Data")]
    [SerializeField] private string useText = "pew pew";
    [SerializeField] private int dmgAmount = 20;
    [SerializeField] private float speed = 1f;
    [SerializeField] private GameObject ammunitionPrefab = null;
    public GameObject AmmunitionPrefab => ammunitionPrefab;

    public override string GetInfoDisplayText()
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Rarity.Name).AppendLine();
        builder.Append("<color=green>Use: ").Append(useText).Append("</color>").AppendLine();
        builder.Append("<color=red>Damge: ").Append(dmgAmount).Append("</color>").AppendLine();
        builder.Append("<color=purple>Speed: ").Append(speed*60).Append("</color>").AppendLine();
        builder.Append("Max Stack: ").Append(MaxStack).AppendLine();

        return builder.ToString();
    }

    public override void Use(GameObject player)
    {
        PlayerController PC = player.GetComponent<PlayerController>();
        PC.Shoot(dmgAmount, ammunitionPrefab, speed);
        Debug.Log($"Shoot {Name}");
    }
}