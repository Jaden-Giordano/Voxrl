using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item {

    Entity owner;

    public string name = "Item";

    public GameObject prefab;

    public Item(Entity owner) {
        this.owner = owner;
    }

}
