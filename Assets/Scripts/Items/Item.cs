using UnityEngine;
using System.Collections;

public class Item {

    Entity owner;

    public string name = "Item";

    public Item(Entity owner) {
        this.owner = owner;
    }

}
