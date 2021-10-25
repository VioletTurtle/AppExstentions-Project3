using UnityEngine;

public interface IHotbarItem
{
    string Name { get; }
    Sprite Icon { get; }
    void Use(GameObject player);
}
