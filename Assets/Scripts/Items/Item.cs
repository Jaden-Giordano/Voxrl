using UnityEngine;
using System.Collections;

public enum Rarity {
    Normal,
    SemiRare,
    Rare,
    SuperRare,
    UltraRare,
    Legendary,
    Unique
}

[System.Serializable]
public class Item {

    public Entity owner;

    public string name = "Item";

    public Rarity rarity = Rarity.Normal;

    public GameObject prefab;

    public Item(Entity owner) {
        this.owner = owner;
    }

}
