using UnityEngine;
using System.Collections;

public class WearableItem : Item {

    protected ItemStats _itemStats;

    public ItemStats itemStats {
        get { return _itemStats; }
        protected set { _itemStats = value; }
    }

    public WearableItem(Entity owner) : base(owner) {
        itemStats = new ItemStats();
    }

}
