using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    protected List<Item> items;

    protected Weapon leftHand;
    protected Weapon rightHand;

    protected WearableItem leftArm;
    protected WearableItem rightArm;

    protected WearableItem neck;

    protected WearableItem chest;

    protected WearableItem shoulders;

    protected WearableItem hands;

    protected WearableItem feet;

    public Inventory() {
        items = new List<Item>();
    }

}
